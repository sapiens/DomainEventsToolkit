using System;
using System.Collections.Generic;

namespace DomainEvents.Managers
{
    internal class Buffer:IDisposable
    {
        private LocalDomainEventsManager _parent;

        public Buffer(LocalDomainEventsManager parent)
        {
            _parent = parent;
        }

        private List<IDomainEvent> _bufferedEvents= new List<IDomainEvent>();

        public void AddEvent(IDomainEvent evnt)
        {
            _bufferedEvents.Add(evnt);
        }

        public void Publish()
        {
            foreach (var evnt in _bufferedEvents)
            {
                _parent.PublishEvent(evnt);
            }
        }

        public void Dispose()
        {
            _parent.EndBuffering();
            _bufferedEvents.Clear();
        }
    }
}