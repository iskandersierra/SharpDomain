using System;
using SharpDomain.Reflection;

namespace SharpDomain.Eventing.ServiceModel.Bus
{
    public interface IEventHandlerRegistry : 
        IMultiItemTypeBasedRegistry<Action<IPublishableEvent>>
    {
    }

    public interface IEventHandlerRegistrar
    {
        IEventHandlerRegistry Registry { get; } 
    }
}