using System;

namespace SharpDomain.Logging
{
    public interface ILogFactory
    {
        LoggingLevel GlobalLevel { get; set; }

        ILog GetNullLog();

        ILog GetLog(string name);

        IDisposable DisableLogging();

        bool IsLoggingEnabled();
    }
}
