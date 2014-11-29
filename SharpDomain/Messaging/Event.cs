namespace SharpDomain.Messaging
{
    public abstract class Event<TId> : IEvent<TId>, IUpdateableEvent
    {
        private TId _sourceId;
        private long _version;

        protected Event()
        {
        }

        public TId SourceId
        {
            get { return _sourceId; }
        }

        public long Version
        {
            get { return _version; }
        }

        object IEvent.SourceId
        {
            get { return SourceId; }
        }

        void IUpdateableEvent.UpdateEvent(object sourceId, long version)
        {
            _sourceId = (TId) sourceId;
            _version = version;
        }
    }
}