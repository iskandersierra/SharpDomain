using System;

namespace SharpDomain.Business
{
    /// <summary>
    /// Allow the creation of aggregate instances.
    /// Different implementation ideas:
    /// [x] ReflectionAggregateFactory: naive ConstructorInfo based factory
    /// [ ] ContainerAggregateFactory: container based aggregate factory
    /// </summary>
    public interface IAggregateFactory
    {
        TAggregate Create<TAggregate>() where TAggregate : class, IAggregate;
    }
}