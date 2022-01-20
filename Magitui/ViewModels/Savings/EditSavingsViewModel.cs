using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magitui.ViewModels.Savings
{

    [QueryProperty(nameof(Id), nameof(Id))]
    public class EditSavingsViewModel : ViewModelBase
    {
        public EditSavingsViewModel()
        {
            _savingsFileService =ServicesProvider.GetService<ISavingsFileService>();
        }

        public ICommand EditSavingsEntryCommand => _editSavingsEntryCommand ??=
           new AsyncCommand(EditSavingsEntryAsync);

       

        private ISavingsFileService _savingsFileService;
        private SavingsDto _savingsDto;
        private ICommand _editSavingsEntryCommand;

        public SavingsDto SavingsDto
        {
            get => _savingsDto;
            set => SetProperty(ref _savingsDto, value);
        }

        public string Id { get; set; }

        public async Task InitializeAsync()
        {
            SavingsDto = await _savingsFileService.ReadItemByIdAsync<SavingsDto>(Guid.Parse(Id));
        }


        private async Task EditSavingsEntryAsync()
        {
            await _savingsFileService.EditItemAsync(SavingsDto);
            await Shell.Current.GoToAsync($"/{nameof(SavingsPage)}");
        }
    }
}
