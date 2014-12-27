namespace SharpDomain.Validation
{
    public enum CommandCompletionStatus
    {
        Succeed,
        InvalidCommand,
        CommandAccessDenied,
        InvalidPreconditions,
        InternalError,
    }
}