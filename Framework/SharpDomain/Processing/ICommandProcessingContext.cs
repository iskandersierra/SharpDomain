using System;
using SharpDomain.Messaging;

namespace SharpDomain.Processing
{
    public interface ICommandProcessingContext
    {
        ICommand Command { get; }

        CommandProcessingError Error { get; }
        void SetError(CommandProcessingError error);

        bool TrySet(Type tokenType, object token, string name);
        bool TryGet(Type tokenType, string name, out object token);
    }
}
