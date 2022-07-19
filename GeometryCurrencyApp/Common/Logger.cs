namespace GeometryCurrencyApp;

/// <summary>
/// Taken from https://stackoverflow.com/questions/40073743/how-to-log-to-a-file-without-using-third-party-logger-in-net-core
/// to not include heavy Nuget loggers to project 
/// </summary>
public class Logger : ILogger
{
    private string _filePath;
    private static object _lock = new object();
    public Logger(string filePath)
    {
        _filePath = filePath;
    }
    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (formatter != null)
        {
            lock (_lock)
            {
                var n = Environment.NewLine;
                string exc = "";
                if (exception != null)
                {
                    exc = n + exception.GetType() + ": " + exception.Message + n + exception.StackTrace + n;
                }                 

                File.AppendAllText(_filePath, logLevel.ToString() + ": " + DateTime.Now.ToString() + " " + formatter(state, exception) + n + exc);
            }
        }
    }
}