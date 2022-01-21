using Magitui.ViewModels.Savings;

namespace Magitui.Pages.Savings
{
    public partial class AddSavingsPage : ContentPage
    {
        private readonly AddSavingsViewModel viewmodel;

        public AddSavingsPage()
        {
            InitializeComponent();
            BindingContext = viewmodel = new AddSavingsViewModel();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewmodel.InitializeAsync();
        }
    }
}