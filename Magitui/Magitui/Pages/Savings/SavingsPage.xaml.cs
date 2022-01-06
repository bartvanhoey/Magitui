using System;
using Magitui.Services.Calculator;
using Magitui.Services.File;
using Magitui.ViewModels.Savings;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace Magitui.Pages
{
    public partial class SavingsPage : ContentPage
    {
        private readonly SavingsViewModel _vm;

        public SavingsPage()
        {
            InitializeComponent();
            var calculatorService = Services.Extensions.ServiceProvider.GetService<ICalculatorService>();
            var savingsFileService = Services.Extensions.ServiceProvider.GetService<ISavingsFileService>();
            BindingContext = _vm = new SavingsViewModel(savingsFileService, calculatorService);

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _vm.OnAppearing();

        }
    }
}