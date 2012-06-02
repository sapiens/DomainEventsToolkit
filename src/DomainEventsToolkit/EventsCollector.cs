using System;
using System.Collections.Generic;

namespace DomainEvents
{
    public class EventsCollector : ICollectEvents
    {
        private Queue<IDomainEvent> _events = new Queue<IDomainEvent>();

        private bool _ignore;

        public void Enable()
        {
            _ignore = false;
        }

        public void Disable()
        {
            _ignore = true;
        }

        public void Add(IDomainEvent ev)
        {
            if (!_ignore) _events.Enqueue(ev);
        }
        public void PublishWith(IPublishDomainEvent publisher)
        {
            if (publisher == null) throw new ArgumentNullException("publisher");
            while (_events.Count > 0)
            {
                publisher.Publish(_events.Dequeue());
            }
        }

        public void Clear()
        {
            _events.Clear();
        }

        public IEnumerable<IDomainEvent> GetPendingEvents()
        {
            return _events;
        }
    }

}