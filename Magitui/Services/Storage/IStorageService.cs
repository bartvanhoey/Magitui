namespace Magitui.Services.Storage;

public interface IStorageService
{
    Task SetGitHubCredentialsAsync((string personalAccessToken, string gitHubUserName, string gitHubBranchName, string gitHubRepositoryName) credentials);
    Task<(string personalAccessToken, string gitHubUserName, string gitHubBranchName, string gitHubRepositoryName)>  GetGitHubCredentialsAsync();
    Task RemoveGitHubCredentialsAsync();
    Task AddOwnerAsync(string owner);
    Task<List<string>> GetOwnersAsync();
}
