using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Media;

namespace SatisfactoryProductionManager;

public abstract partial class ObjectButtonVM<T> : ObservableObject
{
    public T InnerObject { get; protected set; }
    public ImageSource Image { get; protected set; }

    public event Action<T> ObjectSelected;

    [RelayCommand]
    protected virtual void SelectObject()
    {
        ObjectSelected(InnerObject);
    }
}
