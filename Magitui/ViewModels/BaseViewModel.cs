using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Magitui.ViewModels;

public class BaseViewModel :INotifyPropertyChanged 
{
    public event PropertyChangedEventHandler PropertyChanged;

    private bool _isLoadingData;


    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        var handler = PropertyChanged;
        handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public bool IsLoadingData
    {
        get => _isLoadingData;
        set
        {
            _isLoadingData = value;
            OnPropertyChanged();
        }
    }
}