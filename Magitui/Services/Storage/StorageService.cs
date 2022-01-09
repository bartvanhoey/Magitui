using static Magitui.Services.Storage.StorageConsts;

namespace Magitui.Services.Storage;

public class StorageService : StorageServiceBase, IStorageService
{
    private async Task<string> GetGitHubBranchNameAsync()
   => await GetAsync(GitHubBranchName);

    private async Task<string> GetGitHubUserNameAsync()
    => await GetAsync(GitHubUserName);

    private async Task<string> GetPersonalAccessTokenAsync()
        => await GetAsync(PersonalAccessToken);

    private async Task SetGitHubBranchNameAsync(string gitHubBranchName)
    => await SetAsync(GitHubBranchName, gitHubBranchName);

    public async Task SetGitHubCredentialsAsync(string personalAccessToken, string gitHubUserName, string gitHubBranchName)
    {
        await SetPersonalAccessTokenAsync(personalAccessToken);
        await SetGitHubUserNameAsync(gitHubUserName);
        await SetGitHubBranchNameAsync(gitHubBranchName);
    }

    private async Task SetGitHubUserNameAsync(string gitHubUserName)
        => await SetAsync(GitHubUserName, gitHubUserName);

    private async Task SetPersonalAccessTokenAsync(string personalAccessToken)
        => await SetAsync(PersonalAccessToken, personalAccessToken);

    public async Task<(string personalAccessToken, string gitHubUserName, string gitHubBranchName)> GetGitHubCredentialsAsync()
    {
        var pat = await GetPersonalAccessTokenAsync();
        var userName = await GetGitHubUserNameAsync();
        var branchName = await GetGitHubBranchNameAsync();
        return (pat, userName, branchName);
    }
}

