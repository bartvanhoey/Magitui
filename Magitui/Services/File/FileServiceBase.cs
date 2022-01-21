using Magitui.Extensions;
using Magitui.Services.File;
using Magitui.Services.RepoContent;
using Octokit;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;

namespace Magitui.Services.File
{
    public class FileServiceBase
    {
        protected GitHubClient _gitHubClient;
        protected string _repoName;
        protected string _branchName;
        protected string _gitHubUserName;
        protected string _personalAccessToken;
        protected string _appName;
        protected string _nameFileGitHubRepo;
        private IGitHubInfoService _gitHubInfoService;
        protected GitHubInfo _gh;

        public FileServiceBase()
        {
           _gitHubInfoService = ServicesProvider.GetService<IGitHubInfoService>();
        }


        protected async Task<RepositoryContent> GetFileAsync(string fileName)
        {
            if(_gh == null) 
                _gh = await _gitHubInfoService.GetGitHubInfo();

            var contentsByRef = await _gh.Client.GetAllContentsByRef(_gh.UserName, _gh.RepositoryName, _gh.BranchName);
            return contentsByRef.FirstOrDefault(x => x.Name == fileName);
        }

        protected async Task UpdateFileAsync<T>(T item, string dataFile, FileOperation operation) where T : IHaveGuidId
        {
            if (_gh == null)
                _gh = await _gitHubInfoService.GetGitHubInfo();

            try
            {
                var file = await GetFileAsync(dataFile);
                var json = JsonSerializer.Serialize(item);
                var commitMessage = $"commit-{DateTime.Now.ToUniversalTime().ToString(CultureInfo.InvariantCulture)}";
                if (file == null && operation == FileOperation.Add)
                {
                    var createFileRequest = new CreateFileRequest(commitMessage, $"[{json}]", _gh.BranchName);
                    await _gh.Client.CreateFile(_gh.UserName, _gh.RepositoryName, _nameFileGitHubRepo, createFileRequest);
                }
                else
                {
                    var _ = await _gh.Client.GetAllContentsByRef(_gh.UserName, _gh.RepositoryName, _nameFileGitHubRepo, _gh.BranchName);
                    var content = _.FirstOrDefault()?.Content;
                    if (content == null) return;
                    var listOfType = content.ConvertToType<List<T>>();
                    if (listOfType == null) return;

                    if (operation == FileOperation.Add)
                    {
                        item.Id = Guid.NewGuid();
                        listOfType?.Add(item);
                        json = listOfType.ConvertToJson();
                    }
                    else if (operation == FileOperation.Delete)
                    {
                        var itemToDelete = listOfType.FirstOrDefault(x => x.Id == item.Id);
                        if (itemToDelete != null) listOfType.Remove(itemToDelete);
                        json = listOfType.ConvertToJson();
                    }
                    else if (operation == FileOperation.Edit)
                    {
                        var itemToDelete = listOfType.FirstOrDefault(x => x.Id == item.Id);
                        if (itemToDelete != null) listOfType.Remove(itemToDelete);
                        listOfType?.Add(item);
                        json = listOfType.ConvertToJson();
                    }

                    var updateFileRequest = new UpdateFileRequest(commitMessage, json, file.Sha, _gh.BranchName);
                    await _gh.Client.UpdateFile(_gh.UserName, _gh.RepositoryName, _nameFileGitHubRepo, updateFileRequest);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw;
            }
        }

        protected async Task<string> GetFileContentAsync()
        {
            var file = await GetFileAsync(_nameFileGitHubRepo);
            if (file == null) return string.Empty;
            var _ = await _gh.Client.GetAllContentsByRef(_gh.UserName, _gh.RepositoryName, _nameFileGitHubRepo, _gh.BranchName);
            var content = _.FirstOrDefault()?.Content;
            return content;
        }

        public async Task<List<T>> ReadItemsAsync<T>() where T : IHaveGuidId
        {
            try
            {
                var content = await GetFileContentAsync();
                if (string.IsNullOrWhiteSpace(content)) return new List<T>();
                var items = content.ConvertToType<List<T>>();
                if (items == null) return new List<T>();
                return items;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<T> ReadItemByIdAsync<T>(Guid Id) where T : IHaveGuidId
        {
            var items = await ReadItemsAsync<T>();
            var item = items.FirstOrDefault(x => x.Id == Id);
            if (item == null) return default;
            return item;
        }

        public async Task AddItemAsync<T>(T addItem) where T : IHaveGuidId
            => await UpdateFileAsync(addItem, _nameFileGitHubRepo, FileOperation.Add);

        public async Task EditItemAsync<T>(T editItem) where T : IHaveGuidId
            => await UpdateFileAsync(editItem, _nameFileGitHubRepo, FileOperation.Edit);

        public async Task DeleteItemAsync<T>(T deleteItem) where T : IHaveGuidId
            => await UpdateFileAsync(deleteItem, _nameFileGitHubRepo, FileOperation.Delete);

    }

    public enum FileOperation
    {
        Add = 1,
        Edit = 2,
        Delete = 3,
    }

}