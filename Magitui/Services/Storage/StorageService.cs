using static Magitui.Services.Storage.StorageConsts;

namespace Magitui.Services.Storage;

public class StorageService : StorageServiceBase, IStorageService
{
    public async Task SetGitHubCredentialsAsync(string personalAccessToken, string gitHubUserName, string gitHubBranchName, string gitHubRepositoryName)
    {
        await SetPersonalAccessTokenAsync(personalAccessToken);
        await SetGitHubUserNameAsync(gitHubUserName);
        await SetGitHubBranchNameAsync(gitHubBranchName);
        await SetGitHubRepositoryNameAsync(gitHubRepositoryName);
    }
    public async Task<(string personalAccessToken, string gitHubUserName, string gitHubBranchName, string gitHubRepositoryName)> GetGitHubCredentialsAsync()
    {
        var pat = await GetPersonalAccessTokenAsync();
        var userName = await GetGitHubUserNameAsync();
        var branchName = await GetGitHubBranchNameAsync();
        var repoName = await GetGitHubRepositoryNameAsync();
        return (pat, userName, branchName, repoName);
    }

    private async Task SetGitHubRepositoryNameAsync(string gitHubRepositoryName)
        => await SetAsync(GitHubRepositoryName, gitHubRepositoryName);

    private async Task SetGitHubUserNameAsync(string gitHubUserName)
        => await SetAsync(GitHubUserName, gitHubUserName);

    private async Task SetGitHubBranchNameAsync(string gitHubBranchName)
    => await SetAsync(GitHubBranchName, gitHubBranchName);

    private async Task SetPersonalAccessTokenAsync(string personalAccessToken)
        => await SetAsync(PersonalAccessToken, personalAccessToken);

    private async Task<string> GetGitHubRepositoryNameAsync() => await GetAsync(GitHubRepositoryName);

    private async Task<string> GetGitHubBranchNameAsync()
        => await GetAsync(GitHubBranchName);

    private async Task<string> GetGitHubUserNameAsync()
        => await GetAsync(GitHubUserName);

    private async Task<string> GetPersonalAccessTokenAsync()
        => await GetAsync(PersonalAccessToken);


}

