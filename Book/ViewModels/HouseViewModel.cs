using ShareInvest.Models;

using System.ComponentModel;

namespace ShareInvest.ViewModels;

public class HouseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public House? SelectedHouse
    {
        set
        {
            selectedHouse = value;

            OnPropertyChanged(nameof(SelectedHouse));
        }
        get => selectedHouse;
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    House? selectedHouse;
}