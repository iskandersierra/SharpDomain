using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.Messaging
{
    public interface IEvent
    {
        object SourceId { get; }
    }

    public interface IEvent<TSourceId> : IEvent
    {
        new TSourceId SourceId { get; }
    }
}
