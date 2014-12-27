namespace SharpDomain.Aggregates
{
    public abstract class NewIdGeneratorBase<TId> : 
        INewIdGenerator<TId>
    {
        public abstract TId NewId();

        object INewIdGenerator.NewId()
        {
            return NewId();
        }
    }
}