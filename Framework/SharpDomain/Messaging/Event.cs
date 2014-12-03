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

        protected Event(Guid sourceId, int version)
        {
            _sourceId = sourceId;
            _version = version;
        }

        public Guid SourceId
        {
            get { return _sourceId; }
        }

        public int Version
        {
            get { return _version; }
        }

        void IUpdateableEvent.UpdateEvent(Guid sourceId, int version)
        {
            _sourceId = sourceId;
            _version = version;
        }
    }
}