namespace SharpDomain.Commanding
{
    public class ClientCommandBus : CommandBusDecorator
    {
        public ClientCommandBus(ICommandBus commandBusImplementation) 
            : base(commandBusImplementation)
        {
        }
    }
}
