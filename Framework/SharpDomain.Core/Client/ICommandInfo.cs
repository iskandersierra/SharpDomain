using System.Collections.Generic;

namespace SharpDomain.Client
{
    public interface ICommandInfo : ICommandItemInfo
    {
        string Category { get; }
        string LongDescription { get; }
        IReadOnlyCollection<ICommandParameterInfo> Parameters { get; }

        RunCommandResult Execute(string[] args);
    }
}