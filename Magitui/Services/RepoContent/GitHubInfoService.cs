using Magitui.Extensions;
using Magitui.Services.Storage;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magitui.Services.RepoContent
{
    public class GitHubInfoService : IGitHubInfoService
    {
        private readonly IStorageService _storageService;
        private GitHubClient _gitHubClient;

        public GitHubInfoService()
        {
            _storageService = ServicesProvider.GetService<IStorageService>();
        }

        public async Task<GitHubInfo> GetGitHubInfo(string pat = null, string userName = null, string branchName = null, string repoName = null)
        {
            try
            {
                var credentials = await _storageService.GetGitHubCredentialsAsync();

                if (credentials.personalAccessToken.IsNullOrWhiteSpace()) return null;
                if (credentials.gitHubUserName.IsNullOrWhiteSpace()) return null;
                if (credentials.gitHubBranchName.IsNullOrWhiteSpace()) return null;
                if (credentials.gitHubRepositoryName.IsNullOrWhiteSpace()) return null;
                
                _gitHubClient = await GetGitHubClient(credentials);

                if (_gitHubClient == null)
                {
                    return new GitHubInfo { IsAuthorized = false };
                }

                var repoContent = _gitHubClient.Repository.Content;

                return new GitHubInfo
                {
                    IsAuthorized = true,
                    Client = repoContent,
                    UserName = credentials.gitHubUserName,
                    BranchName = credentials.gitHubBranchName,
                    RepositoryName = credentials.gitHubRepositoryName,
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<GitHubInfo> GetGitHubInfo((string personalAccessToken, string userName, string branchName, string repoName) credentials) 
            => await GetGitHubInfo(credentials.personalAccessToken, credentials.userName, credentials.branchName, credentials.repoName);

        private async Task<GitHubClient> GetGitHubClient((string personalAccessToken, string gitHubUserName, string gitHubBranchName, string gitHubRepositoryName) credentials)
        {
            var _gitHubClient = new GitHubClient(new ProductHeaderValue("Magitui"))
            {
                Credentials = new Credentials(credentials.personalAccessToken)
            };
            try
            {
                var user = await _gitHubClient.User.Get(credentials.gitHubUserName);
            }
            catch (AuthorizationException)
            {
                return null;
            }
            return _gitHubClient;
        }
    }
}
