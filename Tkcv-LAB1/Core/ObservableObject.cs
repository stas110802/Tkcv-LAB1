using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Tkcv_LAB1.Core;

public abstract class ObservableObject  : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        if(PropertyChanged != null)
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public virtual bool Set<T> (ref T field, T value, [CallerMemberName] string? name = null)
    {
        if (Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(name);

        return true;
    }
}