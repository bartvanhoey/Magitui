namespace Magitui.Services.File
{
    public interface IFileService {
        Task<List<T>> ReadItemsAsync<T>() where T : IHaveGuidId;
        Task AddItemAsync<T>(T content) where T : IHaveGuidId;
        Task DeleteItemAsync<T>(T content) where T : IHaveGuidId;
        Task EditItemAsync<T>(T content) where T : IHaveGuidId;
    }
}
