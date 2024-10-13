using System.ComponentModel;

namespace ShareInvest.ViewModels;

public class BookViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public DateRangeViewModel? DateRange
    {
        get; set;
    }

    public HouseViewModel? House
    {
        get; set;
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}