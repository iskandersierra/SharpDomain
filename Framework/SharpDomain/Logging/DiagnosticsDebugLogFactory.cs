using System.Diagnostics;

namespace SharpDomain.Logging
{
    public class DiagnosticsDebugLogFactory : SimpleLogFactoryBase
    {
        protected override ILog CreateNamedLog(string name)
        {
            return new DiagnosticsDebugLog(this, name);
        }

        protected class DiagnosticsDebugLog : SimpleLogBase<DiagnosticsDebugLogFactory>
        {
            public DiagnosticsDebugLog(DiagnosticsDebugLogFactory factory, string name)
                : base(factory, name)
            {
                
            }

            protected override void LogInternal(LoggingLevel level, string message)
            {
                Debug.WriteLine(message, level.ToString());
            }
        }
    }
}