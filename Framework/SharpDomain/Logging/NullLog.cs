using System;

namespace SharpDomain.Logging
{
    public sealed class NullLog : ILog
    {
        public NullLog(ILogFactory factory)
        {
            Factory = factory;
            Name = "";
        }

        public string Name { get; private set; }
        public ILogFactory Factory { get; private set; }

        public bool GetIsEnabled(LoggingLevel level)
        {
            return false;
        }

        public void Log<T>(LoggingLevel level, T value)
        {
        }

        public void Log<T>(LoggingLevel level, IFormatProvider formatProvider, T value)
        {
        }

        public void Log(LoggingLevel level, Func<string> messageGenerator)
        {
        }

        public void Log(LoggingLevel level, string message, Exception exception)
        {
        }

        public void Log(LoggingLevel level, IFormatProvider formatProvider, string message, object arg1)
        {
        }

        public void Log(LoggingLevel level, IFormatProvider formatProvider, string message, object arg1, object arg2)
        {
        }

        public void Log(LoggingLevel level, IFormatProvider formatProvider, string message, object arg1, object arg2, object arg3)
        {
        }

        public void Log(LoggingLevel level, IFormatProvider formatProvider, string message, params object[] args)
        {
        }

        public void Log(LoggingLevel level, string message)
        {
        }

        public void Log(LoggingLevel level, string message, object arg1)
        {
        }

        public void Log(LoggingLevel level, string message, object arg1, object arg2)
        {
        }

        public void Log(LoggingLevel level, string message, object arg1, object arg2, object arg3)
        {
        }

        public void Log(LoggingLevel level, string message, params object[] args)
        {
        }
    }
}