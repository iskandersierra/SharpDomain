using System;

namespace SharpDomain.EventSourcing
{
    public interface ICommandProcessorContext
    {
        void Emmit<T>(Action<T> action) 
            where T : class, IEvent;
    }
}