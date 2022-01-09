namespace Magitui.Services.Storage;

public interface IStorageService
{
    Task SetGitHubCredentialsAsync(string personalAccessToken, string gitHubUserName, string gitHubBranchName);
    Task<(string personalAccessToken, string gitHubUserName, string gitHubBranchName)> GetGitHubCredentialsAsync();
}
