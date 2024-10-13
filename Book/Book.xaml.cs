using ShareInvest.EventHandler;
using ShareInvest.Models;
using ShareInvest.ViewModels;

using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

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
                    DataContext = new HouseViewModel
                    {
                        SelectedHouse = page.SelectedHouse
                    };
                }
            }
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