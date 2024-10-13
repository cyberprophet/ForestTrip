using System.ComponentModel;

namespace ShareInvest.ViewModels;

public class DateRangeViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public DateTime? StartDate
    {
        set
        {
            startDate = value;

            OnPropertyChanged(nameof(StartDate));

            UpdateSelectedDateRange();
        }
        get => startDate;
    }

    public DateTime? EndDate
    {
        set
        {
            endDate = value;

            OnPropertyChanged(nameof(EndDate));

            UpdateSelectedDateRange();
        }
        get => endDate;
    }

    public string? SelectedDateRange
    {
        private set
        {
            selectedDateRange = value;

            OnPropertyChanged(nameof(SelectedDateRange));
        }
        get => selectedDateRange;
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    void UpdateSelectedDateRange()
    {
        SelectedDateRange = StartDate.HasValue && EndDate.HasValue ? $"{StartDate:d} ∽ {EndDate:d}" : "날짜선택";
    }

    DateTime? startDate, endDate;

    string? selectedDateRange;
}