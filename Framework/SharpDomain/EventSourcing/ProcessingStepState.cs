namespace SharpDomain.EventSourcing
{
    public enum ProcessingStepState
    {
        Open,
        Committed,
        RolledBack,
        Exception,
    }
}