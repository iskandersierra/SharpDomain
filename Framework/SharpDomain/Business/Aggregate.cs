using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SharpDomain.EventSourcing;
using SharpDomain.Properties;

namespace SharpDomain.Business
{
    public class Aggregate : IAggregate
    {
        private Guid _id = Guid.Empty;
        private int _version = 0;
        private Collection<IEvent> _uncommittedEvents;

        protected Aggregate()
        {
            
        }

        public Guid Id
        {
            get { return _id; }
        }

        public int Version
        {
            get { return _version; }
        }

        public void ApplyEvent(IEvent @event)
        {
            if (@event == null) throw new ArgumentNullException("event");

            if (@event is IAggregateCreatedEvent)
            {
                if (@event.SourceId == Guid.Empty)
                    throw new ArgumentOutOfRangeException("event", @event.SourceId, string.Format(Resources.CannotApplyEmptyIdCreationId, @event.GetType().FullName, this.GetType().FullName));

                _id = @event.SourceId;
                _version = 1;
            }
            else
            {
                if (_id == Guid.Empty)
                    throw new ArgumentOutOfRangeException("event", string.Format(Resources.CannotApplyEventToNewAggregate, @event.GetType().FullName, this.GetType().FullName));
                _version += 1;
            }

            @event.SourceId = _id;
            @event.Version = _version;

            ApplyOverride(@event);

            GetUncommittedEvents().Add(@event);
        }

        protected virtual void ApplyOverride(IEvent @event)
        {
            
        }

        public IEnumerable<IEvent> UncommittedEvents
        {
            get { return GetUncommittedEvents(); }
        }

        private Collection<IEvent> GetUncommittedEvents()
        {
            return _uncommittedEvents ?? (_uncommittedEvents = new Collection<IEvent>());
        }
    }
}
