namespace SharpDomain.Messaging
{
    public abstract class Command : ICommand
    {
        
    }

    public abstract class CorrelatedCommand<TId> : Command, ICorrelatedCommand<TId>
    {
        private readonly TId _correlationId;

        protected CorrelatedCommand(TId correlationId)
        {
            _correlationId = correlationId;
        }

        public TId CorrelationId
        {
            get { return _correlationId; }
        }

        object ICorrelatedCommand.CorrelationId
        {
            get { return CorrelationId; }
        }
    }
}