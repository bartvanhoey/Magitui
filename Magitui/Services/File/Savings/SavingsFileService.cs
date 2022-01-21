using Magitui.Extensions;

namespace Magitui.Services.File
{
    public class SavingsFileService : FileServiceBase, ISavingsFileService
    {
        public SavingsFileService() => _nameFileGitHubRepo = App.AppSettings.SavingsDataFileName;
    }


}
