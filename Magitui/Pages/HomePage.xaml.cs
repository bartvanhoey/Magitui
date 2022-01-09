using System;
using Magitui.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace Magitui.Pages
{
	public partial class HomePage : ContentPage
	{
        private readonly HomeViewModel viewmodel;

        public HomePage()
		{
			InitializeComponent();
			BindingContext = viewmodel = new HomeViewModel();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewmodel.InitializeAsync();
        }
    }
}