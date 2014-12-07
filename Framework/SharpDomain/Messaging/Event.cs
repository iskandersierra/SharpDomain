using System;

namespace SharpDomain.Messaging
{
    public abstract class Event : IEvent, IUpdateableEvent
    {
        private Guid _sourceId;
        private int _version;

        protected Event()
        {
        }

        internal Event(Guid sourceId, int version)
        {
            _sourceId = sourceId;
            _version = version;
        }

        public Guid SourceId
        {
            get { return _sourceId; }
            set { _sourceId = value; }
        }

        public int Version
        {
            get { return _version; }
            set { _version = value; }
        }

        void IUpdateableEvent.UpdateEvent(Guid sourceId, int version)
        {
            UpdateEvent(sourceId, version);
        }

        protected virtual void UpdateEvent(Guid sourceId, int version)
        {
            _sourceId = sourceId;
            _version = version;
        }
    }
}