using System;
using System.Collections;

namespace SharpDomain.Business
{
    public interface IAggregateRepository : IDisposable
    {
        void Fetch(IAggregate newAggregate, Guid id); // up to version #?

        void Save(IAggregate aggregate, Guid commitId);
    }
}
