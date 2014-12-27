using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDomain.Logging;

namespace SharpDomain.Eventing.ServiceModel.Bus
{
    public class InProccessEventBus : IEventBus
    {
        //private readonly Dictionary<Type, List<Action<IPublishedEvent>>> _handlersRegister = new Dictionary<Type, List<Action<IPublishedEvent>>>();
        protected readonly ILog Log;
        protected readonly IEventDispatcher EventDispatcher;

        public InProccessEventBus(IEventDispatcher eventDispatcher, ILogFactory logFactory = null)
        {
            if (eventDispatcher == null) throw new ArgumentNullException("eventDispatcher");
            EventDispatcher = eventDispatcher;
            Log = (logFactory ?? LogManager.Factory).GetCurrentClassLog();
        }

        public void Publish(IPublishableEvent eventMessage)
        {
            EventDispatcher.Dispatch(eventMessage);
        }

        public void Publish(IEnumerable<IPublishableEvent> eventMessages)
        {
            foreach (var @event in eventMessages)
            {
                Publish(@event);
            }
        }
    }
}
