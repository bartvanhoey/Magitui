using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magitui.ViewModels.Savings;

namespace Magitui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSavingsEntryPage : ContentPage
    {
        private readonly AddSavingsEntryViewModel _vm;

        public AddSavingsEntryPage()
        {
            InitializeComponent();
            BindingContext = _vm = new AddSavingsEntryViewModel();

        }

        protected override void OnAppearing()
        {
            _vm.OnAppearing();
        }
    }
}
