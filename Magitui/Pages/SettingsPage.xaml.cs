using Magitui.ViewModels;
using Magitui.ViewModels.Settings;

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