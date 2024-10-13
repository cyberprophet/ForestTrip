using ShareInvest.Models;

using System.Windows;
using System.Windows.Media;

namespace ShareInvest;

partial class RegionHouse : Window
{
    internal RegionHouse(IEnumerable<HouseItem> items)
    {
        InitializeComponent();

        house.ItemsSource = items.Select(e => new House
        {
            Name = e.Name[1..],
            Classification = $"{e.Name[0]}",
            BackgroudColor = (new BrushConverter().ConvertFromString(e.Name[0] switch
            {
                '공' => "#5468C7",
                '국' => "#008504",
                _ => "#AB49AF"
            }) as SolidColorBrush) ?? Brushes.Navy
        });

        house.SelectionChanged += (sender, e) =>
        {
            if (house.SelectedIndex >= 0)
            {
                SelectedHouse = house.SelectedValue as House;

                DialogResult = SelectedHouse != null;

                Close();
            }
        };
    }

    internal House? SelectedHouse
    {
        get; set;
    }
}