using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NLog;

namespace SharpDomain.Logging.NLog
{
    public class NLogFactory : ILogFactory
    {
        private readonly global::NLog.LogFactory nlogFactory;

        public NLogFactory(LogFactory nlogFactory)
        {
            if (nlogFactory == null) throw new ArgumentNullException("nlogFactory");
            this.nlogFactory = nlogFactory;
        }

        public LoggingLevel GlobalLevel
        {
            get
            {
                return LoggingLevelMapper.GetLoggingLevel(nlogFactory.GlobalThreshold);
            }
            set
            {
                nlogFactory.GlobalThreshold = LoggingLevelMapper.GetNLogLevel(value);
            }
        }

        public ILog GetNullLog()
        {
            return new NLogAdapter(nlogFactory.CreateNullLogger(), this);
        }

        public ILog GetLog(string name)
        {
            return new NLogAdapter(nlogFactory.GetLogger(name), this);
        }

        public IDisposable DisableLogging()
        {
            return nlogFactory.DisableLogging();
        }

        public bool IsLoggingEnabled()
        {
            return nlogFactory.IsLoggingEnabled();
        }
    }
}
