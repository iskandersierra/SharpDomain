using System;

namespace SharpDomain.Business
{
    public interface IAggregateRepository : IDisposable
    {
        TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IAggregate;
        
        //TAggregate GetById<TAggregate>(Guid id, int version) where TAggregate : class, IAggregate;

        void Save(IAggregate aggregate, Guid commitId);
    }
}
