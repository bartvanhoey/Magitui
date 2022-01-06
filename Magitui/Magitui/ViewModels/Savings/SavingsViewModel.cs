using Magitui.Models;
using Magitui.Services.Calculator;
using Magitui.Services.File;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Magitui.ViewModels.Savings
{
    public class SavingsViewModel : BaseViewModel
    {

        public SavingsViewModel(ISavingsFileService savingsFileService, ICalculatorService calculatorService)
        {
            _savingsFileService= savingsFileService;
            _calculatorService=calculatorService;
        }

        private readonly ISavingsFileService _savingsFileService;
        private readonly ICalculatorService _calculatorService;
        private ICommand _addSavingsEntryCommand, _loadSavingsCommand, _showDeleteSavingPopupCommand, _editSavingCommand;
        private AddSavingsEntry _selectedAddSavingsEntry;
        private float _totalSavings;

        public ObservableCollection<AddSavingsEntry> SavingsEntries { get; } = new();

        internal async Task OnAppearing() => await LoadSavingsAsync();

        public ICommand AddSavingsEntryCommand => _addSavingsEntryCommand ??=
            new Command(async () => await Shell.Current.GoToAsync(nameof(AddSavingsPage)));

        public ICommand LoadSavingsCommand => _loadSavingsCommand ??=
            new Command(async () => await LoadSavingsAsync());

        public ICommand ShowDeleteSavingPopupCommand => _showDeleteSavingPopupCommand ??=
            new Command<AddSavingsEntry>(async (SelectedItem) => await ShowDeleteSavingPopupAsync(SelectedItem));

        public ICommand EditSavingCommand => _editSavingCommand ??=
            new Command<AddSavingsEntry>(async (SelectedItem) => await  EditSavingAsync(SelectedItem));

        private async Task EditSavingAsync(AddSavingsEntry addSavingsEntry)
        {
            addSavingsEntry.Name = "I Dont KNow";
            await _savingsFileService.EditItemAsync(addSavingsEntry);
            await LoadSavingsAsync();
        }


        private async Task ShowDeleteSavingPopupAsync(AddSavingsEntry addSavingsEntry)
        {
            await _savingsFileService.DeleteItemAsync(addSavingsEntry);
            await LoadSavingsAsync();
        }


        private async Task LoadSavingsAsync()
        {
            IsLoadingData = true;
            var savingsEntries = await _savingsFileService.ReadItemsAsync<AddSavingsEntry>();
            SavingsEntries.Clear();
            foreach (var savingsEntry in savingsEntries) SavingsEntries.Add(savingsEntry);
            TotalSavings = _calculatorService.CalculateTotal(SavingsEntries);
            IsLoadingData = false;

        }

        public AddSavingsEntry SelectedAddSavingsEntry
        {
            get => _selectedAddSavingsEntry;
            set
            {
                _selectedAddSavingsEntry = value;
                OnPropertyChanged();
            }
        }

        public float TotalSavings
        {
            get => _totalSavings;
            set
            {
                _totalSavings = value;
                OnPropertyChanged();
            }
        }

    }
}
