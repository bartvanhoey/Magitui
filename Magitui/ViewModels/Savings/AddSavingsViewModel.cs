
namespace Magitui.ViewModels.Savings
{
    public class AddSavingsViewModel  : ViewModelBase
    {
        private SavingsDto _savingsDto;
        private ICommand _saveSavingsEntryCommand;
        private readonly ISavingsFileService _savingsFileService;

        public ICommand SaveSavingsEntryCommand => _saveSavingsEntryCommand ??=
            new AsyncCommand(SaveSavingsEntryAsync);

        public AddSavingsViewModel()
        {
            SavingsDto = new SavingsDto();
            _savingsFileService = ServicesProvider.GetService<ISavingsFileService>();
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


    }
}
