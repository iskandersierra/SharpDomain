namespace SharpDomain.Commanding
{
    /// <summary>
    /// Represents a domain command requesting the creation of a new aggregate. If combined with 
    /// IRequestCommand, the aggregate id must be returned to the client expecting the response.
    /// </summary>
    public interface ICreationCommand : IDomainCommand
    {
        
    }
}