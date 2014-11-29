namespace SharpDomain.Aggregates
{
    public interface IEntity
    {
        bool IsNew { get; }
    }

    public interface IAggregate : IEntity
    {

    }
    public interface IAggregate<out TId> : IAggregate
    {
        TId Id { get; }
    }
}
