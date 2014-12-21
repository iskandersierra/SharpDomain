using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SharpDomain.EventSourcing;
using SharpDomain.Properties;

namespace SharpDomain.Business
{
    public class Aggregate : IAggregate
    {
        private Collection<IEvent> _uncommittedEvents;

        protected Aggregate()
        {
        }

        public void ApplyEvent(IEvent @event)
        {
            if (@event == null) throw new ArgumentNullException("event");

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
