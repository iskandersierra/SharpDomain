using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SharpDomain.Messaging
{
    public interface ICommandBus
    {
        void Send(Envelope<ICommand> commandMessage);
        void Send(IEnumerable<Envelope<ICommand>> commandMessages);
    }


    [Serializable]
    public class CommandBusExceptionException : Exception
    {
        public CommandBusExceptionException()
        {
        }

        public CommandBusExceptionException(string message)
            : base(message)
        {
        }

        public CommandBusExceptionException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected CommandBusExceptionException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class NoCommandSubscriberFoundException : CommandBusExceptionException
    {
        private readonly string _commandType;

        public string CommandType
        {
            get { return _commandType; }
        }

        public NoCommandSubscriberFoundException(string commandType)
        {
            _commandType = commandType;
        }

        public NoCommandSubscriberFoundException(string commandType, string message)
            : base(message)
        {
            _commandType = commandType;
        }

        public NoCommandSubscriberFoundException(string commandType, string message, Exception inner)
            : base(message, inner)
        {
            _commandType = commandType;
        }

        protected NoCommandSubscriberFoundException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
            _commandType = (string) info.GetValue("CommandType", typeof (string));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("CommandType", CommandType);
        }
    }

    [Serializable]
    public class TooManyCommandSubscribersFoundException : Exception
    {
        private readonly string _commandType;

        public string CommandType
        {
            get { return _commandType; }
        }

        public TooManyCommandSubscribersFoundException(string commandType)
        {
            _commandType = commandType;
        }

        public TooManyCommandSubscribersFoundException(string commandType, string message) : base(message)
        {
            _commandType = commandType;
        }

        public TooManyCommandSubscribersFoundException(string commandType, string message, Exception inner) : base(message, inner)
        {
            _commandType = commandType;
        }

        protected TooManyCommandSubscribersFoundException(
            SerializationInfo info,
            StreamingContext context, string commandType) : base(info, context)
        {
            _commandType = commandType;
        }
    }
}