using Magitui.Services.Storage;

namespace Magitui.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private ICommand _logoutCommand;
        private readonly IStorageService _storageService;

        public SettingsViewModel()
        {
            _storageService = ServicesProvider.GetService<IStorageService>();
        }

        public ICommand LogoutCommand => _logoutCommand ??=
          new AsyncCommand(LogoutAsync);

        private async Task LogoutAsync()
        {
            await _storageService.RemoveGitHubCredentialsAsync();
            await Shell.Current.GoToAsync(nameof(HomePage));
        }
    }
}
