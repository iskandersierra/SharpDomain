using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.Messaging
{
    public interface IMessage
    {
        
    }

    public interface IMessageBus
    {
        void Send(Envelope<IMessage> message);
        void Send(IEnumerable<Envelope<IMessage>> messages);

    }
}
