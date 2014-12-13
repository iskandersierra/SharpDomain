using System;

namespace SharpDomain.EventSourcing
{
    public interface ICommandHandlerContext
    {
        void Emmit<T>(Action<T> action);
    }
}