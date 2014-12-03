namespace SharpDomain.Business
{
    public interface IEventApplier
    {
        void Dispatch(object @event);
    }
}