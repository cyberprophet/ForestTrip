using System.Diagnostics;
using System.Windows;

namespace ShareInvest;

public partial class Book : Window
{
    public Book()
    {
        InitializeComponent();

        webView = new CoreWebView(webView2);

        _ = webView.OnInitializedAsync("https://www.foresttrip.go.kr/main.do");
    }

    void OnStateChanged(object sender, EventArgs _)
    {
#if DEBUG
        Debug.WriteLine(sender);
#endif  
    }

    readonly CoreWebView webView;
}