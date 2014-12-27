using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.Eventing.ServiceModel.Bus
{
    public interface IEventBus
    {
        void Publish(IPublishableEvent eventMessage);
        
        void Publish(IEnumerable<IPublishableEvent> eventMessages);
    }
}
