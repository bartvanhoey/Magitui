using Magitui.ViewModels;

namespace Magitui;

public partial class DesktopShell
{
    public DesktopShell()
    {
        InitializeComponent();

        BindingContext = new ShellViewModel();
    }
}
