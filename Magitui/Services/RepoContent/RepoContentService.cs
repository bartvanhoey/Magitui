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
    public class RepoContentService : IRepoContentService
    {
        private readonly IStorageService _storageService;

        public RepoContentService()
        {
            _storageService = ServicesProvider.GetService<IStorageService>(); 
        }

        public async Task<IRepositoryContentsClient> GetRepositoryContentsClient()
        {
            try
            {
                var _ = await _storageService.GetGitHubCredentialsAsync();

                if (_.personalAccessToken.IsNullOrWhiteSpace()) return null;
                if (_.gitHubUserName.IsNullOrWhiteSpace()) return null;
                if (_.gitHubBranchName.IsNullOrWhiteSpace()) return null;

                var _gitHubClient = new GitHubClient(new ProductHeaderValue("Magitui"))
                {
                    Credentials = new Credentials(_.personalAccessToken)
                };
                var user = _gitHubClient.User.Get(_.gitHubUserName);
                var repoContent = _gitHubClient.Repository.Content;

                return repoContent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
