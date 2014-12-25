using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NLog;

namespace SharpDomain.Logging.NLog
{
    internal class NLogAdapter : ILog
    {
        private readonly global::NLog.Logger nlogLogger;
        private readonly ILogFactory _factory;

        public NLogAdapter(Logger nlogLogger, ILogFactory factory)
        {
            if (nlogLogger == null) throw new ArgumentNullException("nlogLogger");
            if (factory == null) throw new ArgumentNullException("factory");
            this.nlogLogger = nlogLogger;
            _factory = factory;
        }

        public string Name
        {
            get
            {
                return nlogLogger.Name;
            }
        }

        public ILogFactory Factory
        {
            get { return _factory; }
        }

        public bool GetIsEnabled(LoggingLevel level)
        {
            return nlogLogger.IsEnabled(LoggingLevelMapper.GetNLogLevel(level));
        }

        public void Log<T>(LoggingLevel level, T value)
        {
            nlogLogger.Log(LoggingLevelMapper.GetNLogLevel(level), value);
        }

        public void Log<T>(LoggingLevel level, IFormatProvider formatProvider, T value)
        {
            nlogLogger.Log(LoggingLevelMapper.GetNLogLevel(level), formatProvider, value);
        }

        public void Log(LoggingLevel level, Func<string> messageGenerator)
        {
            nlogLogger.Log(LoggingLevelMapper.GetNLogLevel(level), messageGenerator);
        }

        public void Log(LoggingLevel level, string message, Exception exception)
        {
            nlogLogger.Log(LoggingLevelMapper.GetNLogLevel(level), message, exception);
        }

        public void Log(LoggingLevel level, IFormatProvider formatProvider, string message, object arg1)
        {
            nlogLogger.Log(LoggingLevelMapper.GetNLogLevel(level), formatProvider, message, arg1);
        }

        public void Log(LoggingLevel level, IFormatProvider formatProvider, string message, object arg1, object arg2)
        {
            nlogLogger.Log(LoggingLevelMapper.GetNLogLevel(level), formatProvider, message, arg1, arg2);
        }

        public void Log(LoggingLevel level, IFormatProvider formatProvider, string message, object arg1, object arg2, object arg3)
        {
            nlogLogger.Log(LoggingLevelMapper.GetNLogLevel(level), formatProvider, message, arg1, arg2, arg3);
        }

        public void Log(LoggingLevel level, IFormatProvider formatProvider, string message, params object[] args)
        {
            nlogLogger.Log(LoggingLevelMapper.GetNLogLevel(level), formatProvider, message, args);
        }

        public void Log(LoggingLevel level, string message)
        {
            nlogLogger.Log(LoggingLevelMapper.GetNLogLevel(level), message);
        }

        public void Log(LoggingLevel level, string message, object arg1)
        {
            nlogLogger.Log(LoggingLevelMapper.GetNLogLevel(level), message, arg1);
        }

        public void Log(LoggingLevel level, string message, object arg1, object arg2)
        {
            nlogLogger.Log(LoggingLevelMapper.GetNLogLevel(level), message, arg1, arg2);
        }

        public void Log(LoggingLevel level, string message, object arg1, object arg2, object arg3)
        {
            nlogLogger.Log(LoggingLevelMapper.GetNLogLevel(level), message, arg1, arg2, arg3);
        }

        public void Log(LoggingLevel level, string message, params object[] args)
        {
            nlogLogger.Log(LoggingLevelMapper.GetNLogLevel(level), message, args);
        }
    }
}
