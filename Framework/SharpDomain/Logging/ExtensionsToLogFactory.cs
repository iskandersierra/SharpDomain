using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDomain.Reflection;

namespace SharpDomain.Logging
{
    public static class ExtensionsToLogFactory
    {
        public const string DefaultLogName = "default";

        public static ILog GetCurrentClassLog(this ILogFactory factory)
        {
            var log = factory.GetCurrentClassLog(1);
            return log;
        }

        internal static ILog GetCurrentClassLog(this ILogFactory factory, int stackFrameDepth)
        {
            var frame = new StackFrame(stackFrameDepth, false);
            var type = frame.GetMethod().DeclaringType;
            var log = factory.GetLog(type);
            return log;
        }

        public static ILog GetDefaultLog(this ILogFactory factory)
        {
            var log = factory.GetLog(DefaultLogName);
            return log;
        }

        public static ILog GetLog(this ILogFactory factory, Type type)
        {
            var name = type
                .GetTypeName(fullName: true, includeGenerics: false)
                .TurnToCSharpIdentifier(allowDots: true, avoidKeywords: false);
            var log = factory.GetLog(name);
            return log;
        }

        public static ILog GetLog<T>(this ILogFactory factory)
        {
            var log = factory.GetLog(typeof(T));
            return log;
        }
    }
}
