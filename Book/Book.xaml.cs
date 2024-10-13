using ShareInvest.EventHandler;
using ShareInvest.Models;
using ShareInvest.ViewModels;

using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Runtime.Versioning;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ShareInvest;

public partial class Book : Window
{
    public Book()
    {
        InitializeComponent();

        webView = new CoreWebView(webView2);

        np.Text = Convert.ToString(NumberOfPeople);

        webView.Send += (sender, e) =>
        {
            switch (e)
            {
                case HouseArgs h when houses.TryGetValue(h.Item.Region, out List<HouseItem>? list):

                    if (!string.IsNullOrEmpty(h.Item.Id) && !list.Any(e => h.Item.Id.Equals(e.Id)))
                    {
                        list.Add(h.Item);
                    }
                    return;

                case LocationArgs l when !loc.Items.Cast<ComboBoxItem>().Any(item => l.Item.LocName.Equals(item.Content.ToString())):
                    loc.Items.Add(new ComboBoxItem
                    {
                        Content = l.Item.LocName,
                        TabIndex = l.Item.Count,
                        ContentTemplate = (DataTemplate)FindResource("LocItem")
                    });

                    if (houses.ContainsKey(l.Item.LocName) is false)
                    {
                        houses[l.Item.LocName] = [];
                    }
                    return;
            }
        };
        _ = webView.OnInitializedAsync("https://www.foresttrip.go.kr/main.do");
    }

    void OnRegionHouseClick(object sender, RoutedEventArgs e)
    {
        if (loc.SelectedIndex >= 0)
        {
            var key = (loc.SelectedValue as ComboBoxItem)?.Content.ToString();

            if (!string.IsNullOrEmpty(key) && houses.TryGetValue(key, out List<HouseItem>? items) && items.Count > 0)
            {
                var page = new RegionHouse(items)
                {
                    Owner = this
                };

                if (page != null && page.ShowDialog() is bool result && result)
                {
                    DataContext = new BookViewModel
                    {
                        House = new HouseViewModel
                        {
                            SelectedHouse = page.SelectedHouse
                        },
                        DateRange = (DataContext as BookViewModel)?.DateRange
                    };
                }
            }
        }
    }

    void OnClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is DatePicker dp && dp.Template.FindName("sc", dp) is Popup p)
        {
            p.IsOpen = p.IsOpen is false;
        }
    }

    [SupportedOSPlatform("windows")]
    void OnClick(object sender, RoutedEventArgs e)
    {
        if (DataContext is BookViewModel vm && vm.DateRange != null && vm.House != null)
        {
            using (MemoryStream ms = new(Properties.Resources.BINGO))
            {
                using (SoundPlayer sp = new(ms))
                {
                    sp.PlaySync();
                }
            }
        }
    }

    void SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is Calendar calendar)
        {
            DateTime startDate, endDate;

            switch (e.AddedItems.Count)
            {
                case > 1:
                    var items = e.AddedItems.Cast<DateTime>();

                    var sortedItems = items.OrderBy(e => e);

                    startDate = sortedItems.First();
                    endDate = sortedItems.Last();
                    break;

                case 1:
                    var reservationDate = e.AddedItems.Cast<DateTime>().First();

                    startDate = reservationDate;
                    endDate = reservationDate.AddDays(1);
                    break;

                default:
                    return;
            }

            if (calendar.Parent is Border b && b.Parent is Popup p)
            {
                p.IsOpen = false;
            }
            DataContext = new BookViewModel
            {
                DateRange = new DateRangeViewModel
                {
                    StartDate = startDate,
                    EndDate = endDate
                },
                House = (DataContext as BookViewModel)?.House
            };
        }
    }

    void OnIncreaseClick(object _, RoutedEventArgs e)
    {
        if (NumberOfPeople > 0x20)
        {
            return;
        }
        np.Text = Convert.ToString(++NumberOfPeople);
    }

    void OnDecreaseClick(object _, RoutedEventArgs e)
    {
        if (NumberOfPeople < 2)
        {
            return;
        }
        np.Text = Convert.ToString(--NumberOfPeople);
    }

    void OnStateChanged(object sender, EventArgs _)
    {
#if DEBUG
        Debug.WriteLine(sender);
#endif
    }

    void OnClosing(object _, CancelEventArgs e)
    {
        GC.Collect();
    }

    int NumberOfPeople
    {
        get; set;
    }
        = 4;

    readonly CoreWebView webView;

    readonly Dictionary<string, List<HouseItem>> houses = [];
}