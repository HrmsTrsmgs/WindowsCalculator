using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using System.Reflection;

namespace Marimo.WindowsCalculator.Models;

/// <summary>
/// log4netの設定を初期化時に呼び出すための処理です。
/// </summary>
public static class Log4NetExtensions
{
    /// <summary>
    /// log4netの初期化を行います。
    /// </summary>
    /// <remarks>
    /// Debag時はコンソール、リリース時にはファイルに出力します。
    /// </remarks>
    /// <param name="self">登録用のサービス。ここでは使いません。</param>
    /// <param name="logFilePath">出力するファイルのパス。</param>
    /// <returns>登録用のサービス。</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static IServiceCollection AddLog4Net(this IServiceCollection self, string logFilePath)
    {
        var assembly = Assembly.GetEntryAssembly() ?? throw new InvalidOperationException();
        var repository = LogManager.GetRepository(assembly);

        var layout = new PatternLayout
        {
            ConversionPattern = "%date %-5level %logger - %message%n"
        };
        layout.ActivateOptions();

#if DEBUG
        var traceAppender = new TraceAppender { Layout = layout };
        traceAppender.ActivateOptions();
        BasicConfigurator.Configure(repository, traceAppender);
#else
        var fileAppender = new FileAppender
        {
            Layout = layout,
            File = logFilePath,
            AppendToFile = true
        };
        fileAppender.ActivateOptions();
        BasicConfigurator.Configure(repository, fileAppender);
#endif
        return self;
    }
}
