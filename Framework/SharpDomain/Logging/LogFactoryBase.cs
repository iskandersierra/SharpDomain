using System;
using System.Collections.Generic;
using System.Threading;

namespace SharpDomain.Logging
{
    public abstract class LogFactoryBase : ILogFactory
    {
        private ILog _nullLog;
        private Dictionary<string, WeakReference<ILog>> _cachedLoggers = new Dictionary<string, WeakReference<ILog>>();
        private readonly ReaderWriterLockSlim _cacheLocker = new ReaderWriterLockSlim();
        private ICurrentTime _currentTime;

        protected LogFactoryBase() : this(new CurrentLocalTime())
        {
        }

        protected LogFactoryBase(ICurrentTime currentTime)
        {
            _currentTime = currentTime;
        }

        protected DateTime Now()
        {
            return _currentTime.Now();
        }

        public abstract LoggingLevel GlobalLevel { get; set; }

        public ILog GetNullLog()
        {
            return _nullLog ?? (_nullLog = new NullLog(this));
        }

        public ILog GetLog(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) 
                throw new ArgumentNullException("name");

            WeakReference<ILog> weakLog;
            ILog log;

            _cacheLocker.EnterReadLock();
            try
            {
                if (_cachedLoggers.TryGetValue(name, out weakLog))
                    if (weakLog.TryGetTarget(out log))
                        return log;
            }
            finally
            {
                _cacheLocker.EnterReadLock();
            }

            _cacheLocker.EnterWriteLock();
            try
            {
                if (_cachedLoggers.TryGetValue(name, out weakLog))
                    if (weakLog.TryGetTarget(out log))
                        return log;

                log = CreateNamedLog(name);
                weakLog = new WeakReference<ILog>(log);
                _cachedLoggers[name] = weakLog;
                return log;
            }
            finally
            {
                _cacheLocker.ExitWriteLock();
            }
        }

        protected abstract ILog CreateNamedLog(string name);

        public abstract IDisposable DisableLogging();

        public abstract bool IsLoggingEnabled();
    }
}