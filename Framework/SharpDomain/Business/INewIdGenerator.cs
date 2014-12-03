namespace SharpDomain.Aggregates
{
    public interface INewIdGenerator
    {
        object NewId();
    }

    public interface INewIdGenerator<TId> : INewIdGenerator
    {
        new TId NewId();
    }
}
