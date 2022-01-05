using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Magitui.Models;
using Magitui.Services;
using Magitui.Views;
using Magitui.Views.Debts;
using Microsoft.Maui.Controls;

namespace Magitui.ViewModels.Savings
{
    public class SavingsViewModel : BaseViewModel
    {
        private readonly GitHubRepoService _gitHubRepoService;
        private readonly CalculatorService _calculatorService;
        private ICommand _addSavingsEntryCommand, _addDebtEntryCommand,_loadSavingsCommand;
        private AddSavingsEntry _selectedAddSavingsEntry;
        private float _totalSavings;

        public ObservableCollection<AddSavingsEntry> SavingsEntries { get; } = new();

        internal async Task OnAppearing() => await LoadSavingsAsync();

        public ICommand AddSavingsEntryCommand => _addSavingsEntryCommand ??=
            new Command(async () =>  await Shell.Current.GoToAsync(nameof(AddSavingsEntryPage)));

        public ICommand LoadSavingsCommand => _loadSavingsCommand ??=
            new Command(async () => await LoadSavingsAsync());

        public ICommand AddDebtEntryCommand => _addDebtEntryCommand ??=
            new Command(async () => await Shell.Current.GoToAsync(nameof(AddDeptEntryPage)));


        public SavingsViewModel()
        {
            _gitHubRepoService = new GitHubRepoService();
            _calculatorService = new CalculatorService();

        }
        private async Task LoadSavingsAsync()
        {
            IsLoadingData = true;
            var savingsEntries = await _gitHubRepoService.ReadSavingsFileAsync();
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
