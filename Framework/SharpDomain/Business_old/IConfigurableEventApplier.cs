using System;

namespace SharpDomain.Business
{
    public interface IConfigurableEventApplier : IAggregateEventApplier
    {
        void RegisterEvent<TEvent>(Action<TEvent> eventHandler);
    }
}