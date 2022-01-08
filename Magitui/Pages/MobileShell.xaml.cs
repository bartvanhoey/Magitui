using System;
using Magitui.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace Magitui;

public partial class MobileShell
{
    public MobileShell()
    {
        InitializeComponent();

        BindingContext = new ShellViewModel();
    }
}
