using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Magitui.Models;
using Magitui.Services;
using Magitui.Views;
using Microsoft.Maui.Controls;

namespace Magitui.ViewModels.Savings;

public class AddSavingsEntryViewModel : BaseViewModel
{
    private AddSavingsEntry _addSavingsEntry;
    private Command _saveSavingsEntryCommand;
    private readonly GitHubRepoService _repoService;

    public ICommand SaveSavingsEntryCommand => _saveSavingsEntryCommand ??=
        new Command(async () => await SaveSavingsEntryAsync());

    public AddSavingsEntryViewModel()
    {
        _repoService = new GitHubRepoService();
    }

    private async Task SaveSavingsEntryAsync()
    {
        
        await _repoService.UpdateSavingsFileAsync(AddSavingsEntry);
        await Shell.Current.GoToAsync( $"///{nameof(SavingsPage)}");
    }


    public AddSavingsEntry AddSavingsEntry
    {
        get => _addSavingsEntry;
        set
        {
            _addSavingsEntry = value;
            OnPropertyChanged();
        }
    }

    public void OnAppearing()
    {
        AddSavingsEntry = new AddSavingsEntry();
    }
}