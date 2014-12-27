using System.Collections.Generic;

namespace SharpDomain.Validation
{
    public class CommandResult
    {
        public bool Succeed { get; set; }

        public CommandCompletionStatus Status { get; set; }

        public List<CommandError> Errors { get; set; }
    }
}