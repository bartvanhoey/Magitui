using Magitui.ViewModels.Savings;

namespace Magitui.Pages.Savings
{

    public partial class SavingsPage : ContentPage
    {
        private readonly SavingsViewModel viewmodel;

        public SavingsPage()
        {
            InitializeComponent();
            BindingContext = viewmodel = new SavingsViewModel();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewmodel.InitializeAsync();
        }
    }
}