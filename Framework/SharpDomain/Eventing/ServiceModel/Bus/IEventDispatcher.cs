using System;
using System.Collections.Generic;

namespace SharpDomain.Eventing.ServiceModel.Bus
{
    public interface IEventDispatcher
    {
        void Dispatch(IPublishableEvent publishedEvent);
    }

    //public class EventDispatcher : IEventDispatcher
    //{
    //    private IEventHandlerRegistry _eventHandlers;

    //    public EventDispatcher(IEventHandlerRegistry eventHandlers)
    //    {
    //        _eventHandlers = eventHandlers;
    //    }

    //    public void Dispatch(IPublishableEvent publishedEvent)
    //    {
    //        var payloadType = publishedEvent.Payload.GetType();
    //        var allTypes = GetAllInheritedTypes(payloadType);

    //    }

    //    private List<Type> GetAllInheritedTypes(Type type)
    //    {
    //        var list = new List<Type>();

    //        // Put first the types
    //        var currType = type;
    //        while (currType != null)
    //        {
    //            list.Add(currType);
    //            currType = currType.BaseType;
    //        }

    //        // Then the interfaces
    //    }
    //}
}