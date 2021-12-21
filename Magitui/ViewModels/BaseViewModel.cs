using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Magitui.ViewModels;

public class BaseViewModel
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        var handler = PropertyChanged;
        handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}