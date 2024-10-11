using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.ViewModels;
using Microsoft.Extensions.Logging;

namespace Marimo.WindowsCalculator.Calculator
{
    public static class MauiProgram
    {
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
#endif


#if DEBUG
            builder.Logging.SetMinimumLevel(LogLevel.Debug); // デバッグモードでは詳細なログを出力
#else
            builder.Logging.SetMinimumLevel(LogLevel.Information); // リリースモードでは警告以上を出力
#endif

            return builder.Build();
        }
    }
}
