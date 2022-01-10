namespace Magitui.Services.Storage;

public interface IStorageService
{
    Task SetGitHubCredentialsAsync(string personalAccessToken, string gitHubUserName, string gitHubBranchName, string gitHubRepositoryName);
    Task<(string personalAccessToken, string gitHubUserName, string gitHubBranchName, string gitHubRepositoryName)> GetGitHubCredentialsAsync();
}
