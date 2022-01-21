using Magitui.Services.File.Owners;
using Magitui.Services.Storage;

namespace Magitui.ViewModels.Settings
{
    public class SettingsViewModel : ViewModelBase
    {
        private ICommand _logoutCommand, _addOwnerCommand, _showDeleteOwnerPopupCommand;
        private OwnersDto owner, ownerToAdd, selectedOwner;
        private string _ownerName;
        private readonly IStorageService _storageService;
        private readonly IOwnersFileService _ownersFileService;

        public ObservableCollection<OwnersDto> Owners { get; set; } = new();


        public SettingsViewModel()
        {
            _storageService = ServicesProvider.GetService<IStorageService>();
            _ownersFileService = ServicesProvider.GetService<IOwnersFileService>();
        }

        internal async Task InitializeAsync()
        {
            await LoadOwnersAsync();
        }

        public ICommand LogoutCommand => _logoutCommand ??= new AsyncCommand(LogoutAsync);
        public ICommand AddOwnerCommand => _addOwnerCommand ??= new AsyncCommand(AddOwnerAsync);
        public ICommand ShowDeleteOwnerPopupCommand => _showDeleteOwnerPopupCommand ??= new AsyncCommand<OwnersDto>(ShowDeleteOwnerPopupAsync);

        private async Task ShowDeleteOwnerPopupAsync(OwnersDto ownersDto)
        {
            await _ownersFileService.DeleteItemAsync(ownersDto);
            await LoadOwnersAsync();
        }

        private async Task AddOwnerAsync()
        {
            if (string.IsNullOrWhiteSpace(OwnerName)) { return; }
            var ownerExists = Owners.FirstOrDefault(o => o.Name.ToLowerInvariant() == OwnerName.ToLowerInvariant());
            if (ownerExists != null)
            {
                OwnerName = string.Empty;
                return;
            }

            await _ownersFileService.AddItemAsync(new OwnersDto() { Name = OwnerName });
            OwnerName = string.Empty;
            await LoadOwnersAsync();
        }

        private async Task LoadOwnersAsync()
        {
            var owners = await _ownersFileService.ReadItemsAsync<OwnersDto>();
            Owners.Clear();
            foreach (var owner in owners) Owners.Add(owner);
        }

        public string OwnerName
        {
            get => _ownerName;
            set => SetProperty(ref _ownerName, value);
        }

        public OwnersDto Owner
        {
            get => owner;
            set => SetProperty(ref owner, value);
        }

        public OwnersDto SelectedOwner
        {
            get => selectedOwner;
            set => SetProperty(ref selectedOwner, value);
        }


        private async Task LogoutAsync()
        {
            await _storageService.RemoveGitHubCredentialsAsync();
            await Shell.Current.GoToAsync(nameof(HomePage));
        }
    }
}
