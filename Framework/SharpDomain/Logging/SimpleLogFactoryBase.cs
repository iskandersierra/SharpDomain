using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SharpDomain.Logging
{
    public abstract class SimpleLogFactoryBase : LogFactoryBase
    {
        private int _disableLoggingCount = 0;

        protected SimpleLogFactoryBase()
        {
            ShowExceptionsStackTrace = true;
        }

        public override LoggingLevel GlobalLevel { get; set; }

        public bool ShowExceptionsStackTrace { get; set; }

        public override IDisposable DisableLogging()
        {
            return new DisableLoggingGuard(this);
        }

        public override bool IsLoggingEnabled()
        {
            return _disableLoggingCount == 0;
        }

        protected virtual string MessageFormat
        {
            get { return "[{0}] [{1}] {2}"; }
        }

        class DisableLoggingGuard : IDisposable
        {
            private bool _isDisposed;
            private readonly SimpleLogFactoryBase _owner;

            public DisableLoggingGuard(SimpleLogFactoryBase owner)
            {
                _owner = owner;
                owner._disableLoggingCount++;
            }

            public void Dispose()
            {
                if (_isDisposed) return;
                _owner._disableLoggingCount--;
                _isDisposed = true;
            }
        }

        protected abstract class SimpleLogBase<TFactory> : ILog where TFactory : SimpleLogFactoryBase
        {
            protected SimpleLogBase(TFactory factory, string name)
            {
                TypedFactory = factory;
                Name = name;
            }

            protected TFactory TypedFactory { get; private set; }

            public string Name { get; private set; }
            public ILogFactory Factory { get { return TypedFactory; } }

            public bool GetIsEnabled(LoggingLevel level)
            {
                return level >= TypedFactory.GlobalLevel;
            }

            public void Log<T>(LoggingLevel level, T value)
            {
                Log(level, string.Format(CultureInfo.CurrentCulture, "{0}", value));
            }

            public void Log<T>(LoggingLevel level, IFormatProvider formatProvider, T value)
            {
                Log(level, string.Format(formatProvider, "{0}", value));
            }

            public void Log(LoggingLevel level, Func<string> messageGenerator)
            {
                if (!GetIsEnabled(level)) return;
                Log(level, messageGenerator());

            }

            public void Log(LoggingLevel level, string message, Exception exception)
            {
                if (!GetIsEnabled(level)) return;
                message = AppendInfo(message);
                LogInternal(level, message);
                if (TypedFactory.ShowExceptionsStackTrace)
                {
                    var exText = exception.ToString();
                    exText = IndentRight(exText);
                    LogInternal(level, exText);
                }
            }

            public void Log(LoggingLevel level, IFormatProvider formatProvider, string message, object arg1)
            {
                Log(level, string.Format(formatProvider, message, arg1));
            }

            public void Log(LoggingLevel level, IFormatProvider formatProvider, string message, object arg1, object arg2)
            {
                Log(level, string.Format(formatProvider, message, arg1, arg2));
            }

            public void Log(LoggingLevel level, IFormatProvider formatProvider, string message, object arg1, object arg2, object arg3)
            {
                Log(level, string.Format(formatProvider, message, arg1, arg2, arg3));
            }

            public void Log(LoggingLevel level, IFormatProvider formatProvider, string message, params object[] args)
            {
                Log(level, string.Format(formatProvider, message, args));
            }

            public void Log(LoggingLevel level, string message)
            {
                if (!GetIsEnabled(level)) return;
                message = AppendInfo(message);
                LogInternal(level, message);
            }

            public void Log(LoggingLevel level, string message, object arg1)
            {
                Log(level, string.Format(message, arg1));
            }

            public void Log(LoggingLevel level, string message, object arg1, object arg2)
            {
                Log(level, string.Format(message, arg1, arg2));
            }

            public void Log(LoggingLevel level, string message, object arg1, object arg2, object arg3)
            {
                Log(level, string.Format(message, arg1, arg2, arg3));
            }

            public void Log(LoggingLevel level, string message, params object[] args)
            {
                Log(level, string.Format(message, args));
            }

            protected abstract void LogInternal(LoggingLevel level, string message);

            private string AppendInfo(string message)
            {
                return string.Format(TypedFactory.MessageFormat, TypedFactory.Now(), Name, message);
            }

            static readonly Regex NewLineRegex = new Regex(@"\n|^", RegexOptions.Compiled);
            private string IndentRight(string text)
            {
                var result = NewLineRegex.Replace(text, "$0    ");
                return result;
            }
        }
    }
}