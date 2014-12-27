using System;

namespace SharpDomain.Logging
{
    public sealed class NullLogFactory : ILogFactory
    {
        private ILog _nullLog;

        public LoggingLevel GlobalLevel { get; set; }
        public ILog GetNullLog()
        {
            return _nullLog ?? (_nullLog = new NullLog(this));
        }

        public ILog GetLog(string name)
        {
            return GetNullLog();
        }

        public IDisposable DisableLogging()
        {
            return new DummyDisposable();
        }

        public bool IsLoggingEnabled()
        {
            return false;
        }

        class DummyDisposable : IDisposable
        {
            public void Dispose()
            {
                
            }
        }
    }
}