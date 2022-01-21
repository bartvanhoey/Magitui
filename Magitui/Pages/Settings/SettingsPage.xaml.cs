using Magitui.ViewModels;
using Magitui.ViewModels.Settings;

namespace Magitui.Pages.Settings
{
    public partial class SettingsPage : ContentPage
	{
        private readonly SettingsViewModel viewmodel;

        public SettingsPage()
		{
			InitializeComponent();
			BindingContext = viewmodel = new SettingsViewModel();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewmodel.InitializeAsync();
        }
    }
}