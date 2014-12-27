namespace SharpDomain.Eventing.ServiceModel.Bus
{
    public interface IPublishedEvent<out TEvent> : IPublishableEvent
    {
        new TEvent Payload { get; }
    }
}