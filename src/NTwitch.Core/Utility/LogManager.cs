using System;
using System.Threading.Tasks;

namespace NTwitch
{
    public class LogManager
    {
        private LogLevel Level;

        private readonly AsyncEvent<Func<LogMessage, Task>> _logReceivedEvent = new AsyncEvent<Func<LogMessage, Task>>();
        public event Func<LogMessage, Task> LogReceived
        {
            add { _logReceivedEvent.Add(value); }
            remove { _logReceivedEvent.Remove(value); }
        }

        public LogManager(LogLevel level)
        {
            Level = level;
        }

        public async Task LogAsync(LogLevel level, string source, string message, Exception ex = null)
        {
            if (level <= Level)
                await _logReceivedEvent.InvokeAsync(new LogMessage(level, source, message, ex));
        }

        public Task CriticalAsync(string source, Exception ex)
            => LogAsync(LogLevel.Critical, source, ex.Message, ex);
        public Task CriticalAsync(string source, string message, Exception ex = null)
            => LogAsync(LogLevel.Critical, source, message, ex);

        public Task ErrorAsync(string source, Exception ex)
            => LogAsync(LogLevel.Error, source, ex.Message, ex);
        public Task ErrorAsync(string source, string message, Exception ex = null)
            => LogAsync(LogLevel.Error, source, message, ex);

        public Task WarningAsync(string source, Exception ex)
            => LogAsync(LogLevel.Warning, source, ex.Message, ex);
        public Task WarningAsync(string source, string message, Exception ex = null)
            => LogAsync(LogLevel.Warning, source, message, ex);

        public Task InfoAsync(string source, Exception ex)
            => LogAsync(LogLevel.Info, source, ex.Message, ex);
        public Task InfoAsync(string source, string message, Exception ex = null)
            => LogAsync(LogLevel.Info, source, message, ex);

        public Task DebugAsync(string source, Exception ex)
            => LogAsync(LogLevel.Debug, source, ex.Message, ex);
        public Task DebugAsync(string source, string message, Exception ex = null)
            => LogAsync(LogLevel.Debug, source, message, ex);
    }
}
