using Magitui.ViewModels.Savings;

namespace Magitui
{
    public partial class AddSavingsPage : ContentPage
	{
        private readonly AddSavingsViewModel viewmodel;

        public AddSavingsPage()
		{
			InitializeComponent();
			BindingContext = viewmodel= new AddSavingsViewModel();
		}

    


    }
}