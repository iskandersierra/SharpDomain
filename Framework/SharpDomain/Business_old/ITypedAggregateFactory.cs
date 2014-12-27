namespace SharpDomain.Business
{
    public interface ITypedAggregateFactory<out TAggregate>
        where TAggregate : class, IAggregate
    {
        TAggregate Create();
    }
}