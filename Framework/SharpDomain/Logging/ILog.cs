namespace SharpDomain.Logging
{
    using System;

    public interface ILog
    {
        string Name { get; }

        ILogFactory Factory { get; }

        #region [ Log ]

        bool GetIsEnabled(LoggingLevel level);

        void Log<T>(LoggingLevel level, T value);

        void Log<T>(LoggingLevel level, IFormatProvider formatProvider, T value);

        void Log(LoggingLevel level, Func<string> messageGenerator);

        void Log(LoggingLevel level, string message, Exception exception);

        void Log(LoggingLevel level, IFormatProvider formatProvider, string message, object arg1);

        void Log(LoggingLevel level, IFormatProvider formatProvider, string message, object arg1, object arg2);
        
        void Log(LoggingLevel level, IFormatProvider formatProvider, string message, object arg1, object arg2, object arg3);

        void Log(LoggingLevel level, IFormatProvider formatProvider, string message, params object[] args);

        void Log(LoggingLevel level, string message);

        void Log(LoggingLevel level, string message, object arg1);

        void Log(LoggingLevel level, string message, object arg1, object arg2);

        void Log(LoggingLevel level, string message, object arg1, object arg2, object arg3);

        void Log(LoggingLevel level, string message, params object[] args);

        #endregion [ Log ]

    }
}
