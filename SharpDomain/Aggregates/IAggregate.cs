namespace SharpDomain.Aggregates
{
    public interface IAggregate : IEntity
    {

    }
    public interface IAggregate<out TId> : IAggregate
    {
        TId Id { get; }
    }
}
