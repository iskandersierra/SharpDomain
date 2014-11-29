using System;

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

    public abstract class NewIdGeneratorBase<TId> : 
        INewIdGenerator<TId>
    {
        public abstract TId NewId();

        object INewIdGenerator.NewId()
        {
            return NewId();
        }
    }


    public class NewGuidGenerator : NewIdGeneratorBase<Guid>
    {
        public override Guid NewId()
        {
            return Guid.NewGuid();
        }
    }
}
