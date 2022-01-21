using Magitui.ViewModels.Savings;

namespace Magitui.Pages.Savings;

public partial class EditSavingsPage : ContentPage
{
    private EditSavingsViewModel viewmodel;

    public EditSavingsPage()
	{
		InitializeComponent();

		BindingContext = viewmodel = new EditSavingsViewModel();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewmodel.InitializeAsync();
    }
}