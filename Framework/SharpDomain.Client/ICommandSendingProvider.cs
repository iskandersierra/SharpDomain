using System.Collections.Generic;

namespace SharpDomain.Client
{
    public interface ICommandSendingProvider
    {
        IReadOnlyCollection<ICommandInfo> Commands { get; }
    }
}