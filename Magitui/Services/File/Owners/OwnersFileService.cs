namespace Magitui.Services.File.Owners
{
    public class OwnersFileService : FileServiceBase, IOwnersFileService
    {
        public OwnersFileService()
        {
            _nameFileGitHubRepo = App.AppSettings.OwnersDataFileName;
        }
    }
}
