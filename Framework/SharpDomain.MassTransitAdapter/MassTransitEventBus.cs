using System;
using System.Collections.Generic;
using Common.Logging;
using MassTransit;
using SharpDomain.Messaging;

namespace SharpDomain.MassTransitAdapter
{
    public class MassTransitEventBus : IEventBus
    {
        private readonly IServiceBus _serviceBus;
        private readonly ILog _logger;

        protected IServiceBus ServiceBus
        {
            get { return _serviceBus; }
        }

        protected ILog Logger
        {
            get { return _logger; }
        }

        public MassTransitEventBus(IServiceBus serviceBus)
        {
            if (serviceBus == null) throw new ArgumentNullException("serviceBus");
            this._serviceBus = serviceBus;

            Logger.Info("CREATED MassTransitEventBus");
        }

        public void Publish(Envelope<IEvent> eventMessage)
        {
            if (eventMessage == null) throw new ArgumentNullException("eventMessage");
            ServiceBus.Publish(eventMessage);

            Logger.Debug("PUBLISH event " + eventMessage);
        }

        public void Publish(IEnumerable<Envelope<IEvent>> eventMessages)
        {
            if (eventMessages == null) throw new ArgumentNullException("eventMessages");
            foreach (var eventMessage in eventMessages)
            {
                Publish(eventMessage);
            }
        }
    }
}