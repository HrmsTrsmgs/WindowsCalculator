using Marimo.WindowsCalculator.MauiBlazor;
using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.ViewModels;
using Microsoft.Extensions.Logging;

namespace Marimo.WindowsCalculator.MauiBlazor;

public static class MauiProgram
{
    /// <summary>
    /// アプリケーションの初期化時に呼び出されます。
    /// </summary>
    /// <returns>初期化されたApp。</returns>
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });
        builder.Services.AddSingleton<CalculatorViewModel>();

        builder.Services.AddMauiBlazorWebView();

        builder.Services.AddLog4Net(Path.Combine(AppContext.BaseDirectory, "log.txt"));

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);
#else
        builder.Logging.SetMinimumLevel(LogLevel.Information);
#endif
        return builder.Build();
    }
}
