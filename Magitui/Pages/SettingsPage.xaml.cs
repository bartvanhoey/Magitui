using Magitui.ViewModels;

namespace Magitui.Pages
{
    public partial class SettingsPage : ContentPage
	{
		public SettingsPage()
		{
			InitializeComponent();
			BindingContext = new SettingsViewModel();
		}
	}
}