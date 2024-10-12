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
            if (!loc.Items.Cast<ComboBoxItem>().Any(item => e.LocName.Equals(item.Content.ToString())))
            {
                loc.Items.Add(new ComboBoxItem
                {
                    Content = e.LocName,
                    TabIndex = e.Count,
                    ContentTemplate = (DataTemplate)FindResource("LocItem")
                });
            }
        };
        _ = webView.OnInitializedAsync("https://www.foresttrip.go.kr/main.do");
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

    int NumberOfPeople
    {
        get; set;
    }
        = 4;

    readonly CoreWebView webView;
}