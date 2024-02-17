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

        Init();
    }

    public void Init()
    {
        try
        {
            var task = Task.Run(() => webApp.Run(portNumber));
            Webview.Source = new Uri($"http://localhost:{portNumber}?apiPort={portNumber}");
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            if (Debugger.IsAttached)
                Debugger.Break();
        }
    }
}