using Magitui.Services.Storage;

namespace Magitui.ViewModels.Settings
{
    public class SettingsViewModel : ViewModelBase
    {
        private ICommand _logoutCommand, _addOwnerCommand;
        private string owner, ownerToAdd;
        private readonly IStorageService _storageService;
        public ObservableCollection<string> Owners { get; set; } = new ObservableCollection<string>() { "Jakob", "Bart"};


        public SettingsViewModel()
        {
            _storageService = ServicesProvider.GetService<IStorageService>();
        }

        public ICommand LogoutCommand => _logoutCommand ??= new AsyncCommand(LogoutAsync);
        public ICommand AddOwnerCommand => _addOwnerCommand ??= new AsyncCommand(AddOwnerAsync);

        private Task AddOwnerAsync()
        {
            var owners = Owners.ToList();
            owners.Add(OwnerToAdd);
            Owners.Clear();
            foreach (var owner in owners)
                Owners.Add(owner);

            return Task.CompletedTask;

        }

        public string Owner { 
            get => owner; 
            set => SetProperty(ref owner, value); }


        public string OwnerToAdd
        {
            get => ownerToAdd;
            set => SetProperty(ref ownerToAdd, value);
        }

        private async Task LogoutAsync()
        {
            await _storageService.RemoveGitHubCredentialsAsync();
            await Shell.Current.GoToAsync(nameof(HomePage));
        }
    }
}
