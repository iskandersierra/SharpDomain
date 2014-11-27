using System.Collections.Generic;

namespace SharpDomain.Messaging
{
    public interface ICommandBus
    {
        void Send(Envelope<ICommand> eventMessage);
        void Send(IEnumerable<Envelope<ICommand>> eventMessages);
    }
}