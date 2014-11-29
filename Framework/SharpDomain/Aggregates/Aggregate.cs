using System;
using System.Collections.Generic;
using System.Linq;
using SharpDomain.Messaging;

namespace SharpDomain.Aggregates
{
    public abstract class Aggregate<TId> : IAggregate<TId>
    {
        private readonly TId _id;
        private long _version = -1;
        private bool _isNew;
        private readonly List<IEvent<TId>> _pendingEvents;
        private readonly Dictionary<Type, Action<IEvent<TId>>> _eventHandlers;

        protected Aggregate(TId id)
        {
            _id = id;
            _isNew = true;
            _pendingEvents = new List<IEvent<TId>>();
            _eventHandlers = new Dictionary<Type, Action<IEvent<TId>>>();
        }

        public TId Id
        {
            get { return _id; }
        }

        public IEnumerable<IEvent<TId>> Events
        {
            get
            {
                return _pendingEvents.AsEnumerable();
            }
        }

        public long Version
        {
            get { return _version; }
        }

        public bool IsNew
        {
            get
            {
                return _isNew;
            }
        }

        protected void Handles<TEvent>(Action<TEvent> eventHandler)
            where TEvent : IEvent<TId>
        {
            if (eventHandler == null) throw new ArgumentNullException("eventHandler");
            _eventHandlers.Add(typeof(TEvent), e => eventHandler((TEvent) e));
        }

        protected void LoadFrom(IEnumerable<IEvent<TId>> pastEvents)
        {
            if (pastEvents == null) throw new ArgumentNullException("pastEvents");

            // If this objects is being loaded from past events then it is not a new one
            _isNew = false;
            foreach (var pastEvent in pastEvents)
                ApplyEvent(pastEvent);
        }

        protected void UpdateFrom(IEvent<TId> newEvent)
        {
            if (newEvent == null) throw new ArgumentNullException("newEvent");
            
            var updEv = newEvent as IUpdateableEvent;
            if (updEv != null)
                updEv.UpdateEvent(Id, Version + 1);
            else
                throw new InvalidOperationException("Only non-persisted events can be applied to this domain object");

            ApplyEvent(newEvent);

            _pendingEvents.Add(newEvent);
        }

        private void ApplyEvent(IEvent<TId> pastEvent)
        {
            InvokeEvent(pastEvent);
            _version = pastEvent.Version;
        }

        private void InvokeEvent(IEvent<TId> pastEvent)
        {
            _eventHandlers[pastEvent.GetType()].Invoke(pastEvent);
        }

        void IAggregate<TId>.AppendEvent(IEvent<TId> @event)
        {
            UpdateFrom(@event);
        }

        void IAggregate.AppendEvent(IEvent @event)
        {
            UpdateFrom((IEvent<TId>)@event);
        }

        IEnumerable<IEvent> IAggregate.Events
        {
            get
            {
                return Events;
            }
        }

        object IAggregate.Id
        {
            get { return Id; }
        }
    }
}