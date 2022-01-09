using Octokit;

namespace Magitui.Services.RepoContent
{
    public interface IRepoContentService
    {
        Task<IRepositoryContentsClient> GetRepositoryContentsClient();
    }
}