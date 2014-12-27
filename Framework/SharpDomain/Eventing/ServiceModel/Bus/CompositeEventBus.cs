using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.Eventing.ServiceModel.Bus
{
    public class CompositeEventBus : IEventBus
    {
        private readonly List<IEventBus> _busses = new List<IEventBus>();

        public CompositeEventBus()
        {
        }

        public void Include(IEventBus bus)
        {
            if (bus == null) throw new ArgumentNullException("bus");
            if (_busses.Contains(bus)) return;

            _busses.Add(bus);
        }

        public void Exclude(IEventBus bus)
        {
            if (bus == null) throw new ArgumentNullException("bus");

            _busses.Remove(bus);
        }

        public void Publish(IPublishableEvent eventMessage)
        {
            foreach (var bus in _busses)
            {
                bus.Publish(eventMessage);
            }
        }

        public void Publish(IEnumerable<IPublishableEvent> eventMessages)
        {
            foreach (var bus in _busses)
            {
                bus.Publish(eventMessages);
            }
        }
    }
}
