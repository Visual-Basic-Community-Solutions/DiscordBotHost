using Android.Util;
using DiscordBotHost.Services;
using Microsoft.Extensions.Logging;

namespace DiscordBotHost.Platforms.Android
{
    public class AndroidLogger<T> : ILogger<T>
    {
        private readonly ILogger _logger;
        private readonly LoggingService _loggingService;

        public AndroidLogger(LoggingService service,ILoggerFactory factory)
        {
            _loggingService = service;
            _logger = factory.CreateLogger(typeof(T).Name);
        }

        IDisposable ILogger.BeginScope<TState>(TState state) => _logger.BeginScope(state);

        bool ILogger.IsEnabled(LogLevel logLevel) => _logger.IsEnabled(logLevel);

        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var caller = typeof(T).Name;
            var tag = $"Disqord<{caller}>:{eventId}";
            var message = formatter(state, exception);

            switch (logLevel)
            {
                case LogLevel.Trace:
                    Log.Verbose(tag, message);
                    _loggingService.TryWrite($"<strong>{logLevel}</strong>: {message}");
                    break;
                case LogLevel.Debug:
                    Log.Debug(tag, message);
                    _loggingService.TryWrite($"<strong>{logLevel}</strong>: {message}");
                    break;
                case LogLevel.Information:
                    Log.Info(tag, message);
                    _loggingService.TryWrite($"<strong>{logLevel}</strong>: {message}");
                    break;
                case LogLevel.Warning:
                    Log.Warn(tag, message);
                    _loggingService.TryWrite($"<strong>{logLevel}</strong>: {message}");
                    break;
                case LogLevel.Error:
                    Log.Error(tag, $"{message}: {exception.Message}");
                    _loggingService.TryWrite($"<strong>{logLevel}</strong>: {message} - <span style='color:#F44336'>{exception.Message}</span>");
                    break;
                case LogLevel.Critical:
                    Log.Wtf(tag, $"{message}: {exception}");
                    _loggingService.TryWrite($"<strong>{logLevel}</strong>: {message} - <span style='color:#F44336'>{exception}</span>");
                    break;
                case LogLevel.None:
                    break;
                default:
                    break;
            }
        }
    }
}
