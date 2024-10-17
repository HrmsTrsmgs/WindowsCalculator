using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using System;
using System.IO;

namespace Marimo.WindowsCalculator.WinUI3;


/// <summary>
/// アプリケーションです。
/// </summary>
public partial class App : Application
{
    //:引継ぎ事項:
    // Rereaceモードで起動しません。
    // 原因追及には時間がかかると思われます。

    IHost Host { get; }
    Window m_window;
    
    public App()
    {

        this.InitializeComponent();

        Host = new HostBuilder()
           .ConfigureServices((context, services) =>
           {
               services.AddLog4Net(Path.Combine(AppContext.BaseDirectory, "log.txt"));
               services.AddSingleton<MainWindow>();
               services.AddSingleton<CalculatorViewModel>();
           })
           .Build();
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        m_window = Host.Services.GetRequiredService<MainWindow>();
        m_window.Activate();
    }
}
