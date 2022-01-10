namespace Magitui.ViewModels.Savings
{
    public class SavingsViewModel : ViewModelBase
    {
        public SavingsViewModel()
        {
            _calculatorService = ServicesProvider.GetService<ICalculatorService>();
            _savingsFileService =ServicesProvider.GetService<ISavingsFileService>();
        }

        private readonly ISavingsFileService _savingsFileService;
        private readonly ICalculatorService _calculatorService;
        private ICommand _addSavingsEntryCommand, _loadSavingsCommand, _showDeleteSavingPopupCommand, _editSavingCommand;
        private SavingsDto _selectedAddSavingsEntry;
        private float _totalSavings;

        public ObservableCollection<SavingsDto> SavingsEntries { get; } = new();

        internal async Task InitializeAsync() => await LoadSavingsAsync();

        public ICommand AddSavingsEntryCommand => _addSavingsEntryCommand ??=
            new AsyncCommand(async () => await Shell.Current.GoToAsync(nameof(AddSavingsPage)));

        public ICommand LoadSavingsCommand => _loadSavingsCommand ??= new AsyncCommand(LoadSavingsAsync);

        public ICommand ShowDeleteSavingPopupCommand => _showDeleteSavingPopupCommand ??= new AsyncCommand<SavingsDto>(ShowDeleteSavingPopupAsync);

        public ICommand EditSavingCommand => _editSavingCommand ??= new AsyncCommand<SavingsDto>(EditSavingAsync);

        private async Task EditSavingAsync(SavingsDto addSavingsEntry)
        {
            addSavingsEntry.Name = "I Dont KNow";
            await _savingsFileService.EditItemAsync(addSavingsEntry);
            await LoadSavingsAsync();
        }


        private async Task ShowDeleteSavingPopupAsync(SavingsDto addSavingsEntry)
        {
            await _savingsFileService.DeleteItemAsync(addSavingsEntry);
            await LoadSavingsAsync();
        }


        private async Task LoadSavingsAsync()
        {
            IsLoadingData = true;
            var savingsEntries = await _savingsFileService.ReadItemsAsync<SavingsDto>();
            SavingsEntries.Clear();
            foreach (var savingsEntry in savingsEntries) SavingsEntries.Add(savingsEntry);
            TotalSavings = _calculatorService.CalculateTotal(SavingsEntries);
            IsLoadingData = false;
        }

        public SavingsDto SelectedAddSavingsEntry
        {
            get => _selectedAddSavingsEntry;
            set => SetProperty(ref _selectedAddSavingsEntry, value);
        }

        public float TotalSavings
        {
            get => _totalSavings;
            set => SetProperty(ref _totalSavings, value);
        }

    }
}
