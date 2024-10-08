using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using System.Reflection;

namespace Marimo.MauiBlazor.Models;

public static class Log4NetExtensions
{
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
