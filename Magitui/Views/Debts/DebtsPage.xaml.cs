using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magitui.ViewModels;

namespace Magitui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DebtsPage : ContentPage
    {
        private readonly DebtsViewModel _vm;
        public DebtsPage()
        {
            InitializeComponent();
            BindingContext = _vm= new DebtsViewModel();

        }
    }
}
