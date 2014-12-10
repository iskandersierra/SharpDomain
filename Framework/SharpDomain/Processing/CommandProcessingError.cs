using System;

namespace SharpDomain.Processing
{
    public class CommandProcessingError
    {
        public CommandProcessingError(Exception exception, string step) : this(exception)
        {
            Step = step;
        }

        public CommandProcessingError(Exception exception)
        {
            if (exception == null) throw new ArgumentNullException("exception");
            Exception = exception;
        }

        public Exception Exception { get; private set; }
        public string Step { get; private set; }
    }
}