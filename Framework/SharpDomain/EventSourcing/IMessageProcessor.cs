using System;

namespace SharpDomain.EventSourcing
{
    public interface IMessageProcessor
    {
        //void Process(IMessage message);
    }

    public interface IMessageProcessingStep : IDisposable
    {
        // ctor(IMessageProcessorContext context, ... other services from the container ...)

        void Commit();
        void Rollback();
    }
}