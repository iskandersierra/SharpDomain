using NServiceBus;
using SharpDomain.Commanding;

namespace SharpDomain.NServiceBus.Commanding
{
    public class NSBAccessCommandHeaders : IAccessCommandHeader
    {
        private IBus _bus;
        private IDomainCommand _command;

        public NSBAccessCommandHeaders(IBus bus, IDomainCommand command)
        {
            _bus = bus;
            _command = command;
        }

        public string GetHeader(string key)
        {
            return _bus.GetMessageHeader(_command, key);
        }
    }
}