namespace SharpDomain.Business
{
    public interface IAggregateEventApplier : IEventApplier
    {
        void RegisterAggregate(IAggregate aggregate);
    }
}