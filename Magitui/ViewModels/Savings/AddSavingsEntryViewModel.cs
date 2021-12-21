using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Magitui.Models;
using Magitui.Services;
using Microsoft.Maui.Controls;

namespace Magitui.ViewModels.Savings;

public class AddSavingsEntryViewModel : BaseViewModel
{
    private string _belongsTo;
    private string _name;
    private float _amount;
    private string _company;
    private DateTime _createdAt;
    private DateTime _savedAt;
    private DateTime _editedAt;
    private AddSavingsEntry _addSavingsEntry;
    private Command _saveSavingsEntryCommand;


    public ICommand SaveSavingsEntryCommand => _saveSavingsEntryCommand ??=
        new Command(async () => await SaveSavingsEntryAsync());

    private async Task SaveSavingsEntryAsync()
    {
        var repoService = new GitHubRepoService();

        var savingsEntry = new AddSavingsEntry()
        {
            Name = "Gezamelijke Rekening",
            Amount = 100f,
            Company = "AXA",
            BelongsTo = "Bart",
            CreatedAt = DateTime.Now,
            SavedAt = DateTime.Now,
            EditedAt = DateTime.Now,
        };
        await repoService.UpdateSavingsFileAsync<AddSavingsEntry>(savingsEntry);
    }

    //public string BelongsTo
    //{
    //    get => _belongsTo;
    //    set
    //    {
    //        _belongsTo = value;
    //        OnPropertyChanged();
    //    }
    //}
    //public string Name { 
    //    get => _name ;
    //    set
    //    {
    //        _name = value;
    //        OnPropertyChanged();
    //    }
    //}

    //public float Amount
    //{
    //    get => _amount;
    //    set
    //    {
    //        _amount = value;
    //        OnPropertyChanged();
    //    }
    //}

    //public string Company
    //{
    //    get => _company;
    //    set
    //    {
    //        _company = value;
    //        OnPropertyChanged();
    //    }
    //}

    //public DateTime CreatedAt
    //{
    //    get => _createdAt;
    //    set
    //    {
    //        _createdAt = value;
    //        OnPropertyChanged();
    //    }
    //}

    //public DateTime SavedAt
    //{
    //    get => _savedAt;
    //    set
    //    {
    //        _savedAt = value;
    //        OnPropertyChanged();
    //    }
    //}

    //public DateTime EditedAt
    //{
    //    get => _editedAt;
    //    set
    //    {
    //        _editedAt = value;
    //        OnPropertyChanged();
    //    }
    //}

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