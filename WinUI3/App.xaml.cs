using Marimo.WindowsCalculator.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Marimo.WindowsCalculator.WinUI3;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    IHost Host { get; }
    Window m_window;
    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {

        this.InitializeComponent();


        Host = new HostBuilder()
           .ConfigureServices((context, services) =>
           {
               services.AddSingleton<MainWindow>();
               services.AddSingleton<CalculatorViewModel>();
           })
           .Build();
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        m_window = Host.Services.GetRequiredService<MainWindow>();
        m_window.Activate();
    }
}
