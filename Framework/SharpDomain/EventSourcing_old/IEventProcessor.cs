namespace SharpDomain.EventSourcing
{
    public interface IEventProcessor : IMessageProcessor
    {
    }

    public interface IEventProcessor<in TEvent>
        where TEvent : class, IEvent
    {
        void Process(TEvent @event, IEventProcessorContext context);
    }
}