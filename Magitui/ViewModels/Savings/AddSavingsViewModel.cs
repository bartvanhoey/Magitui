
using Magitui.Pages.Savings;
using Magitui.Services.File.Owners;

namespace Magitui.ViewModels.Savings
{
    public class AddSavingsViewModel : ViewModelBase
    {
        private SavingsDto _savingsDto;
        private ICommand _saveSavingsEntryCommand;
        private readonly ISavingsFileService _savingsFileService;
        private readonly IOwnersFileService _ownersFileService;
        private OwnersDto _selectedOwnersDto;

        public ObservableCollection<OwnersDto> OwnersDtos { get; set; } = new();

        public ICommand SaveSavingsEntryCommand => _saveSavingsEntryCommand ??=
            new AsyncCommand(SaveSavingsEntryAsync);

        public async Task InitializeAsync()
        {
            var ownersDtos = await _ownersFileService.ReadItemsAsync<OwnersDto>();
            OwnersDtos.Clear();
            foreach (var owner in ownersDtos) OwnersDtos.Add(owner);
        }

        public AddSavingsViewModel()
        {
            SavingsDto = new SavingsDto();
            _savingsFileService = ServicesProvider.GetService<ISavingsFileService>();
            _ownersFileService = ServicesProvider.GetService<IOwnersFileService>();
        }

        private async Task SaveSavingsEntryAsync()
        {
            await _savingsFileService.AddItemAsync(SavingsDto);
            await Shell.Current.GoToAsync($"/{nameof(SavingsPage)}");
        }

        public SavingsDto SavingsDto
        {
            get => _savingsDto;
            set => SetProperty(ref _savingsDto, value);
        }

        public OwnersDto SelectedOwnersDto
        {
            get => _selectedOwnersDto;
            set
            {
                SetProperty(ref _selectedOwnersDto, value);
                if (_selectedOwnersDto != null && !string.IsNullOrWhiteSpace(_selectedOwnersDto.Name))
                {
                    SavingsDto.BelongsTo = value.Name;
                }
            }
        }

    }
}
