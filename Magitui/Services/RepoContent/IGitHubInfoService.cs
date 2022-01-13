using Octokit;

namespace Magitui.Services.RepoContent
{
    public interface IGitHubInfoService
    {
        Task<GitHubInfo> GetGitHubInfo(string personalAccessToken = null, string userName = null, string branchName = null, string repoName = null);
        Task<GitHubInfo> GetGitHubInfo((string personalAccessToken, string userName, string branchName, string repoName) credentials);
    }

    public class GitHubInfo {
        public bool IsAuthorized { get; set; } 
        public IRepositoryContentsClient Client { get; set; }
        public string UserName { get; set; }
        public string BranchName { get; set; }
        public string RepositoryName { get; internal set; }
    }
}