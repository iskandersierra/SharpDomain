using System;
using SharpDomain.Reflection;

namespace SharpDomain.Eventing.ServiceModel.Bus
{
    public class EventHandlerRegistry : 
        ThreadSafeMultiItemTypeBasedRegistryBase<Action<IPublishableEvent>>, 
        IEventHandlerRegistry
    {
    }
}