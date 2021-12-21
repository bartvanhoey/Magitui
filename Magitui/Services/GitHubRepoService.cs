using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Magitui.Configuration;
using Octokit;

namespace Magitui.Services
{
    public class GitHubRepoService : IRepoService
    {
        private readonly GitHubClient _gitHubClient;
        private readonly string _repoName;
        private readonly string _branchName;
        private readonly string _gitHubUserName;
        private readonly string _personalAccessToken;
        private readonly string _appName;
        private readonly string _savingsDataFile;
        private readonly IRepositoryContentsClient _repoContent;

        public GitHubRepoService()
        {
            _repoName = App.AppSettings.RepoName;
            _branchName = App.AppSettings.BranchName;
            _gitHubUserName = App.AppSettings.GitHubUserName;
            _appName = App.AppSettings.AppName;
            _savingsDataFile = App.AppSettings.SavingsDataFileName;


            try
            {
                _personalAccessToken = App.AppSettings.PersonalAccessToken;
                _gitHubClient = new GitHubClient(new ProductHeaderValue(_appName))
                {
                    Credentials = new Credentials(_personalAccessToken)
                };
                var user = _gitHubClient.User.Get(_gitHubUserName);
                _repoContent = _gitHubClient.Repository.Content;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        private async Task<RepositoryContent> GetFileAsync(string fileName)
        {
            var contentsByRef = await _repoContent.GetAllContentsByRef(_gitHubUserName, _repoName, _branchName);
            return contentsByRef.FirstOrDefault(x => x.Name == fileName);
        }

        public async Task<string> ReadSavingsFileAsync()
        {

            throw new NotImplementedException();

        }




        public async Task UpdateSavingsFileAsync<T>(T content)
        {
            await UpdateFileAsync(content, _savingsDataFile);
        }

        private async Task UpdateFileAsync<T>(T contentToAdd, string dataFile)
        {
            try
            {
                var file = await GetFileAsync(dataFile);
                var json = JsonSerializer.Serialize(contentToAdd);
                var commitMessage = $"commit-{DateTime.Now.ToUniversalTime().ToString(CultureInfo.InvariantCulture)}";
                if (file == null)
                {
                    var createFileRequest = new CreateFileRequest(commitMessage, $"[{json}]", _branchName);
                    await _repoContent.CreateFile(_gitHubUserName, _repoName, _savingsDataFile, createFileRequest);
                }
                else
                {
                    var _ = await _repoContent.GetAllContentsByRef(_gitHubUserName, _repoName, _savingsDataFile, _branchName);
                    var content = _.FirstOrDefault()?.Content;
                    if (content == null) return;
                    var listOfType = JsonSerializer.Deserialize<List<T>>(content);
                    if (listOfType == null) return;
                    listOfType?.Add(contentToAdd);
                    json = JsonSerializer.Serialize(listOfType);
                    var updateFileRequest = new UpdateFileRequest(commitMessage, json, file.Sha, _branchName);
                    await _repoContent.UpdateFile(_gitHubUserName, _repoName, _savingsDataFile, updateFileRequest);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw;
            }
        }

        public Task<string> ReadDebtsFileAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateDebtsFileAsync(string content)
        {
            throw new NotImplementedException();
        }
    }
}
