using Magitui.Extensions;

namespace Magitui.Services.File
{
    public class SavingsFileService : FileServiceBase, ISavingsFileService
    {
        public SavingsFileService() => _savingsDataFile = App.AppSettings.SavingsDataFileName;

        public async Task<List<T>> ReadItemsAsync<T>() where T : IHaveGuidId
        {
            try
            {
                var content = await GetFileContentAsync();
                if (content == null) return new List<T>();
                var items = content.ConvertToType<List<T>>();
                if (items == null) return new List<T>();
                return items;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<string> GetFileContentAsync()
        {
            var file = await GetFileAsync(_savingsDataFile);
            if (file == null) return string.Empty;
            var _ = await _gh.Client.GetAllContentsByRef(_gh.UserName, _gh.RepositoryName, _savingsDataFile, _gh.BranchName);
            var content = _.FirstOrDefault()?.Content;
            return content;
        }

        public async Task<T> ReadItemByIdAsync<T>(Guid Id) where T : IHaveGuidId
        {
            var items = await ReadItemsAsync<T>();
            var item = items.FirstOrDefault(x => x.Id == Id);
            if (item == null) return default;
            return item;
        }

        public async Task AddItemAsync<T>(T addItem) where T : IHaveGuidId
            => await UpdateFileAsync(addItem, _savingsDataFile, FileOperation.Add);

        public async Task EditItemAsync<T>(T editItem) where T : IHaveGuidId
            => await UpdateFileAsync(editItem, _savingsDataFile, FileOperation.Edit);

        public async Task DeleteItemAsync<T>(T deleteItem) where T : IHaveGuidId
            => await UpdateFileAsync(deleteItem, _savingsDataFile, FileOperation.Delete);


    }


}
