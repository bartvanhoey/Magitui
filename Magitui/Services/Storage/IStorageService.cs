namespace Magitui.Services.Storage;

public interface IStorageService
{
    Task SetPersonalAccessTokenAsync(string personalAccessToken);
    Task<string> GetPersonalAccessTokenAsync();
}
