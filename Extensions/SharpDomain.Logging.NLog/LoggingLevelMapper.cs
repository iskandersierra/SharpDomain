using NLog;

namespace SharpDomain.Logging.NLog
{
    internal static class LoggingLevelMapper
    {
        public static LoggingLevel GetLoggingLevel(global::NLog.LogLevel level)
        {
            return NLogToSharpDomainMap[level.Ordinal];
        }

        public static global::NLog.LogLevel GetNLogLevel(LoggingLevel level)
        {
            return LogLevel.FromOrdinal((int) level);
        }

        private static readonly LoggingLevel[] NLogToSharpDomainMap = new[]
        {
            LoggingLevel.Trace,
            LoggingLevel.Debug,
            LoggingLevel.Info,
            LoggingLevel.Warn,
            LoggingLevel.Error,
            LoggingLevel.Fatal,
            LoggingLevel.Off,
        };
    }
}