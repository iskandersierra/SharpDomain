using System;

namespace SharpDomain.Logging
{
    public class ColoredConsoleLogFactory : SimpleLogFactoryBase
    {
        public ColoredConsoleLogFactory()
        {
            TraceForeground = ConsoleColor.DarkGray;
            DebugForeground = ConsoleColor.Gray;
            InfoForeground = ConsoleColor.White;
            WarnForeground = ConsoleColor.DarkYellow;
            ErrorForeground = ConsoleColor.DarkRed;
            FatalForeground = ConsoleColor.Red;
        }

        protected override ILog CreateNamedLog(string name)
        {
            return new ColoredConsoleLog(this, name);
        }

        public ConsoleColor? TraceForeground { get; set; }
        public ConsoleColor? DebugForeground { get; set; }
        public ConsoleColor? InfoForeground { get; set; }
        public ConsoleColor? WarnForeground { get; set; }
        public ConsoleColor? ErrorForeground { get; set; }
        public ConsoleColor? FatalForeground { get; set; }

        protected class ColoredConsoleLog : SimpleLogBase<ColoredConsoleLogFactory>
        {
            public ColoredConsoleLog(ColoredConsoleLogFactory factory, string name)
                : base(factory, name)
            {
            }

            protected override void LogInternal(LoggingLevel level, string message)
            {
                var oldFore = Console.ForegroundColor;

                switch (level)
                {
                    case LoggingLevel.Trace:
                        Console.ForegroundColor = TypedFactory.TraceForeground.GetValueOrDefault(oldFore);
                        break;
                    case LoggingLevel.Debug:
                        Console.ForegroundColor = TypedFactory.DebugForeground.GetValueOrDefault(oldFore);
                        break;
                    case LoggingLevel.Info:
                        Console.ForegroundColor = TypedFactory.InfoForeground.GetValueOrDefault(oldFore);
                        break;
                    case LoggingLevel.Warn:
                        Console.ForegroundColor = TypedFactory.WarnForeground.GetValueOrDefault(oldFore);
                        break;
                    case LoggingLevel.Error:
                        Console.ForegroundColor = TypedFactory.ErrorForeground.GetValueOrDefault(oldFore);
                        break;
                    case LoggingLevel.Fatal:
                        Console.ForegroundColor = TypedFactory.FatalForeground.GetValueOrDefault(oldFore);
                        break;
                }

                Console.WriteLine(message);

                Console.ForegroundColor = oldFore;
            }
        }
    }
}