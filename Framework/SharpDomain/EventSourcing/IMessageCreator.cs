using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.EventSourcing
{
    public interface IMessageCreator
    {
        Type GetConcreteMessageType(Type messageType);
        Type GetInterfaceMessageType(Type messageType);
    }
}
