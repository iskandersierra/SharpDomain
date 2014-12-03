using System;
using System.Collections.Generic;
using System.Globalization;
using Common.Logging;
using MassTransit;
using SharpDomain.Messaging;
using SharpDomain.Properties;

namespace SharpDomain.MassTransitAdapter
{
    public class MassTransitCommandBus : ICommandBus
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

        public MassTransitCommandBus(IServiceBus serviceBus, ILog logger)
        {
            if (serviceBus == null) throw new ArgumentNullException("serviceBus");
            if (logger == null) throw new ArgumentNullException("logger");
            this._serviceBus = serviceBus;
            this._logger = logger;

            Logger.Info("CREATED MassTransitCommandBus");
        }

        public void Send(Envelope<ICommand> commandMessage)
        {
            if (commandMessage == null) throw new ArgumentNullException("commandMessage");
            ServiceBus.Publish(commandMessage, OnPublishContextCallback);

            Logger.Debug("SEND command " + commandMessage);
        }

        public void Send(IEnumerable<Envelope<ICommand>> commandMessages)
        {
            if (commandMessages == null) throw new ArgumentNullException("commandMessages");
            foreach (var message in commandMessages)
            {
                Send(message);
            }
        }

        private void OnPublishContextCallback(IPublishContext<Envelope<ICommand>> context)
        {
            // Set correlation id to message if command has one
            var correlationId = GetCorrelationId(context.Message);
            context.SetCorrelationId(correlationId);

            // Verify command is at least received by one command handler
            context.IfNoSubscribers(() =>
            {
                var errorMessage = string.Format(Resources.CommandWasNotReceivedByAnySubscriber, context.Message);

                Logger.Error(errorMessage);

                throw new NoCommandSubscriberFoundException(context.Message.Body.GetType().FullName, errorMessage);
            });
            
            // Verify command is not received by more than one command handler
            var addresses = new List<IEndpointAddress>();
            context.ForEachSubscriber(address =>
            {
                if (addresses == null) addresses = new List<IEndpointAddress>();
                addresses.Add(address);
                if (addresses.Count >= 2)
                {
                    var errorMessage = string.Format(Resources.TooManyEndpointsForCommand, context.Message, string.Join(", ", addresses));

                    Logger.Error(errorMessage);

                    throw new TooManyCommandSubscribersFoundException(context.Message.Body.GetType().FullName, errorMessage);
                }
            });
        }

        private string GetCorrelationId(Envelope<ICommand> message)
        {
            var correlatedCommand = message.Body as ICorrelatedCommand;
            if (correlatedCommand != null)
                return string.Format(CultureInfo.InvariantCulture, "{0}", correlatedCommand.CorrelationId);
            return null;
        }

        private void NoSubscribersFoundForCommand(Envelope<ICommand> message)
        {
            var errorMessage = string.Format(Resources.CommandWasNotReceivedByAnySubscriber, message);

            Logger.Error(errorMessage);

            throw new NoCommandSubscriberFoundException(message.Body.GetType().FullName, errorMessage);
        }
    }
}
