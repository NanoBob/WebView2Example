using ExampleApp.Web;
using System.Diagnostics;
using System.Windows;

namespace ExampleApp.Ui;

public partial class MainWindow : Window
{
    private readonly WebApp webApp = new();
    private const int portNumber = 20332;

    public MainWindow()
    {
        InitializeComponent();

        _ = Task.Run(() => webApp.Run(portNumber));
        Webview.Source = new Uri($"http://localhost:{portNumber}");
    }
}
