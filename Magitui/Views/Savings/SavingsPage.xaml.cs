using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magitui.Services;
using Magitui.ViewModels;
using Magitui.ViewModels.Savings;

namespace Magitui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SavingsPage : ContentPage
    {

        private readonly SavingsViewModel _vm;

        public SavingsPage()
        {
            InitializeComponent();
            BindingContext= _vm = new SavingsViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _vm.OnAppearing();

        }


    }
}
