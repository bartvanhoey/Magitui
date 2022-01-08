using static Magitui.Services.Storage.StorageConsts;

namespace Magitui.Services.Storage;

public class StorageService : StorageServiceBase, IStorageService
{
    public async Task<string> GetPersonalAccessTokenAsync()
        => await GetAsync(PersonalAccessToken);

    public async Task SetPersonalAccessTokenAsync(string personalAccessToken)
        => await SetAsync(PersonalAccessToken, personalAccessToken);
}

