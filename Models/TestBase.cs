using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using log4net.Repository;
using log4net;

namespace Marimo.WindowsCalculator.Models;

public class TestBase
{
    protected MemoryAppender MemoryAppender { get; init; }
    public TestBase()
    {
        MemoryAppender = new()
        {
            Layout = new PatternLayout("%message%n")
        };
        MemoryAppender.ActivateOptions();


        ILoggerRepository repository = LogManager.GetRepository(GetType().Assembly);
        BasicConfigurator.Configure(repository, MemoryAppender);
    }
}
