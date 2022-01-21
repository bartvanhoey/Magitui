using static Magitui.Services.Storage.StorageConsts;

namespace Magitui.Services.Storage;

public class StorageService : StorageServiceBase, IStorageService
{
    public async Task SetGitHubCredentialsAsync((string personalAccessToken, string gitHubUserName, string gitHubBranchName, string gitHubRepositoryName) credentials)
    {
        await SetPersonalAccessTokenAsync(credentials.personalAccessToken);
        await SetGitHubUserNameAsync(credentials.gitHubUserName);
        await SetGitHubBranchNameAsync(credentials.gitHubBranchName);
        await SetGitHubRepositoryNameAsync(credentials.gitHubRepositoryName);
    }
    public async Task<(string personalAccessToken, string gitHubUserName, string gitHubBranchName, string gitHubRepositoryName)> GetGitHubCredentialsAsync()
    {
        var pat = await GetPersonalAccessTokenAsync();
        var userName = await GetGitHubUserNameAsync();
        var branchName = await GetGitHubBranchNameAsync();
        var repoName = await GetGitHubRepositoryNameAsync();
        return (pat, userName, branchName, repoName);
    }

    public async Task RemoveGitHubCredentialsAsync()
    {
        await RemoveAsync(GitHubRepositoryName);
        await RemoveAsync(GitHubUserName);
        await RemoveAsync(GitHubBranchName);
        await RemoveAsync(PersonalAccessToken);
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

    public async Task AddOwnerAsync(string owner)
    {
        if (string.IsNullOrWhiteSpace(owner)) return;
        owner = owner.Replace(";", "");
        var listOwners = await GetOwnersAsync();
        listOwners.Add(owner);
        var owners = string.Join(";", listOwners);
        await SetAsync(Owners, owners);
    }

    public async Task<List<string>> GetOwnersAsync()
    {

        var commaSeparatedOwners = await GetAsync(Owners);


        var listOwners = new List<string>();
        if (!string.IsNullOrWhiteSpace(commaSeparatedOwners))
        {
            var separated = commaSeparatedOwners.Split(';').ToList();
            if (separated.Any())
            {
                listOwners.AddRange(separated);
            }
        }
        return listOwners;
    }
}

