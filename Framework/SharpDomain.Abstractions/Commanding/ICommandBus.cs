namespace SharpDomain.Commanding
{
    /// <summary>
    /// Service to send command to a target server or distributor
    /// </summary>
    public interface ICommandBus
    {
        /// <summary>
        /// Sends the command to a target server or distributor
        /// </summary>
        /// <param name="command"></param>
        void Send(IDomainCommand command);
    }
}
