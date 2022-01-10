using Octokit;

namespace Magitui.Services.RepoContent
{
    public interface IGitHubInfoService
    {
        Task<GitHubInfo> GetGitHubInfo(string pat = null, string userName = null, string branchName = null, string repoName = null);
    }

    public class GitHubInfo {
        public IRepositoryContentsClient Client { get; set; }
        public string UserName { get; set; }
        public string BranchName { get; set; }
        public string RepositoryName { get; internal set; }
    }
}