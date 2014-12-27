using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.Eventing
{
    public interface IEvent
    {
        Guid EventId { get; }

        DateTime EventTimeStamp { get; }

        long EventVersion { get; }
    }
}
