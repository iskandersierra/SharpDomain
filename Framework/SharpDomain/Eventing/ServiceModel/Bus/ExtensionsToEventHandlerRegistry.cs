using System;

namespace SharpDomain.Eventing.ServiceModel.Bus
{
    public static class ExtensionsToEventHandlerRegistry
    {
        public static void Register<TEvent>(this IEventHandlerRegistry registry, IEventHandler<TEvent> handler)
        {
            if (registry == null) throw new ArgumentNullException("registry");
            if (handler == null) throw new ArgumentNullException("handler");
            
            var eventDataType = typeof(TEvent);

            Action<IPublishableEvent> action = ev => handler.Handle((IPublishedEvent<TEvent>) ev);

            registry.Register(eventDataType, action);
        }
    }
}