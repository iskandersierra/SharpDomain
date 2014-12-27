using System;
using Magnum;

namespace SharpDomain.Aggregates
{
    public class NewCombGuidGenerator : NewIdGeneratorBase<Guid>, INewGuidGenerator
    {
        public override Guid NewId()
        {
            return CombGuid.Generate();
        }
    }
}