using System;

namespace SharpDomain.Aggregates
{
    public class NewGuidGenerator : NewIdGeneratorBase<Guid>, INewGuidGenerator
    {
        public override Guid NewId()
        {
            return Guid.NewGuid();
        }
    }
}