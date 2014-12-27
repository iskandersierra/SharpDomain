using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDomain.Reflection;

namespace SharpDomain.Logging
{
    public static class LogManager
    {

        private static Lazy<ILogFactory> _factory;
        private static ILogFactory _nullFactory;
        private static ConsoleLogFactory _consoleFactory;
        private static ColoredConsoleLogFactory _coloredConsoleFactory;
        private static DiagnosticsTraceLogFactory _traceFactory;
        private static DiagnosticsDebugLogFactory _debugFactory;


        public static ILog GetNullLog()
        {
            return Factory.GetNullLog();
        }

        public static ILog GetLog(string name)
        {
            return Factory.GetLog(name);
        }

        public static IDisposable DisableLogging()
        {
            return Factory.DisableLogging();
        }

        public static bool IsLoggingEnabled()
        {
            return Factory.IsLoggingEnabled();
        }

        public static ILog GetCurrentClassLog()
        {
            return Factory.GetCurrentClassLog(2);
        }

        public static ILog GetDefaultLog()
        {
            return Factory.GetDefaultLog();
        }

        public static ILog GetLog(Type type)
        {
            return Factory.GetLog(type);
        }

        public static ILog GetLog<T>()
        {
            return Factory.GetLog<T>();
        }

        public static ILogFactory Factory
        {
            get
            {
                if (_factory == null)
                    return NullFactory;
                    //throw new InvalidOperationException("Default log factory has not been set");
                return _factory.Value;
            }
        }

        public static ILogFactory NullFactory
        {
            get
            {
                return _nullFactory ?? (_nullFactory = new NullLogFactory());
            }
        }

        public static DiagnosticsTraceLogFactory TraceFactory
        {
            get
            {
                return _traceFactory ?? (_traceFactory = new DiagnosticsTraceLogFactory());
            }
        }

        public static DiagnosticsDebugLogFactory DebugFactory
        {
            get
            {
                return _debugFactory ?? (_debugFactory = new DiagnosticsDebugLogFactory());
            }
        }

        public static ConsoleLogFactory ConsoleFactory
        {
            get
            {
                return _consoleFactory ?? (_consoleFactory = new ConsoleLogFactory());
            }
        }

        public static ColoredConsoleLogFactory ColoredConsoleFactory
        {
            get
            {
                return _coloredConsoleFactory ?? (_coloredConsoleFactory = new ColoredConsoleLogFactory());
            }
        }

        public static void SetFactory(ILogFactory factory)
        {
            if (factory == null) throw new ArgumentNullException("factory");

            SetFactory(() => factory);
        }
        public static void SetFactory(Func<ILogFactory> factoryBuilder)
        {
            if (factoryBuilder == null) throw new ArgumentNullException("factoryBuilder");
            if (_factory != null) throw new InvalidOperationException("Cannot set default log factory more than once");

            _factory = new Lazy<ILogFactory>(factoryBuilder, true);
        }


    }
}
