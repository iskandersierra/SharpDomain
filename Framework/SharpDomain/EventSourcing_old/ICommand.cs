namespace SharpDomain.EventSourcing
{
    public interface ICommand : IMessage
    {
    }

    //public interface ICorrelatedCommand : ICommand
    //{
    //    object CorrelationId { get; }
    //}

    //public interface ICorrelatedCommand<TId> : ICorrelatedCommand
    //{
    //    TId CorrelationId { get; }
    //}
}
