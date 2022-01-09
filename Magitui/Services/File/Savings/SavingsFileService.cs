using Magitui.Extensions;
using Octokit;

namespace Magitui.Services.File
{
    public class SavingsFileService : FileServiceBase, ISavingsFileService
    {
        public SavingsFileService()
        {
            _repoName = App.AppSettings.RepoName;
            _branchName = App.AppSettings.BranchName;
            //_gitHubUserName = App.AppSettings.GitHubUserName;
            //_appName = App.AppSettings.AppName;
            _savingsDataFile = App.AppSettings.SavingsDataFileName;

            //try
            //{
            //    _personalAccessToken = App.AppSettings.PersonalAccessToken;
            //    _gitHubClient = new GitHubClient(new ProductHeaderValue(_appName))
            //    {
            //        Credentials = new Credentials(_personalAccessToken)
            //    };
            //    var user = _gitHubClient.User.Get(_gitHubUserName);
            //    _repoContent = _gitHubClient.Repository.Content;
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    throw;
            //}
        }

        public async Task<List<T>> ReadItemsAsync<T>() where T : IHaveGuidId
        {
            try
            {
                var file = await GetFileAsync(_savingsDataFile);
                if (file == null) return new List<T>();

                var _ = await _repoContent.GetAllContentsByRef(_gitHubUserName, _repoName, _savingsDataFile, _branchName);
                var content = _.FirstOrDefault()?.Content;
                if (content == null) return new List<T>();
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
           
        public async Task AddItemAsync<T>(T addItem) where T : IHaveGuidId
        {
            await UpdateFileAsync(addItem, _savingsDataFile, FileOperation.Add);
        }

        public async Task EditItemAsync<T>(T editItem) where T : IHaveGuidId
        {
            await UpdateFileAsync(editItem, _savingsDataFile, FileOperation.Edit);
        }

        public async Task DeleteItemAsync<T>(T deleteItem) where T : IHaveGuidId
        {
            await UpdateFileAsync(deleteItem, _savingsDataFile, FileOperation.Delete);
        }

      
    }

    
}
