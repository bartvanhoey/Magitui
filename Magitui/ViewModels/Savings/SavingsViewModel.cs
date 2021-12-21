using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Magitui.Models;
using Magitui.Services;
using Microsoft.Maui.Controls;

namespace Magitui.ViewModels.Savings
{
    public class SavingsViewModel : BaseViewModel
    {
        private GitHubRepoService _gitHubRepoService;
        private ICommand _addSavingsEntryCommand;
        private ICommand _addDebtEntryCommand;
        private ICommand _refreshCommand;
        private AddSavingsEntry _selectedAddSavingsEntry;
        private bool _isBusy;

        public ICommand RefreshCommand => _refreshCommand ??= new Command(FetchSavingsEntries);

        private void FetchSavingsEntries()
        {
            Debug.WriteLine("I am refreshing");
        }


        public ICommand AddSavingsEntryCommand => _addSavingsEntryCommand ??=
            new Command(async () =>  await Shell.Current.GoToAsync("addSavingsEntry"));
        
        public ICommand AddDebtEntryCommand => _addDebtEntryCommand ??=
            new Command(async () => await Shell.Current.GoToAsync("addDebtEntry"));


        public ObservableCollection<AddSavingsEntry> SavingsEntries { get; } = new();

        public SavingsViewModel()
        {
            _gitHubRepoService = new GitHubRepoService();
            //var read = _gitHubRepoService.ReadSavingsFileAsync();

            SavingsEntries.Add(new AddSavingsEntry { Name = "Gezamelijke rekening", Amount = 2900f });
            SavingsEntries.Add(new AddSavingsEntry { Name = "Gezamelijke spaarrekening", Amount = 10000 });
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



        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }


    }
}
