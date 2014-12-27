namespace SharpDomain.Eventing.ServiceModel.Bus
{
    public interface IEventHandler<in TEvent>
    {
        void Handle(IPublishedEvent<TEvent> @event);
    }
}