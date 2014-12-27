using System.Diagnostics;

namespace SharpDomain.Logging
{
    public class DiagnosticsTraceLogFactory : SimpleLogFactoryBase
    {
        protected override ILog CreateNamedLog(string name)
        {
            return new DiagnosticsTraceLog(this, name);
        }

        protected class DiagnosticsTraceLog : SimpleLogBase<DiagnosticsTraceLogFactory>
        {
            public DiagnosticsTraceLog(DiagnosticsTraceLogFactory factory, string name)
                : base(factory, name)
            {
                
            }

            protected override void LogInternal(LoggingLevel level, string message)
            {
                Trace.WriteLine(message, level.ToString());
            }
        }
    }
}