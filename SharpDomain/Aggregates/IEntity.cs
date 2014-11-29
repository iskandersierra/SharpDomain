namespace SharpDomain.Aggregates
{
    /// <summary>
    /// This interface represents the base interface for domain objects which not necessarily 
    /// are aggregate objects
    /// </summary>
    public interface IEntity
    {
        bool IsNew { get; }
    }
}