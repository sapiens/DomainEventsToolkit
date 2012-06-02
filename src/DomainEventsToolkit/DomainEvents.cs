using System;
using DomainEvents.Managers;

namespace DomainEvents
{
    public static class DomainEvents
    {
        [ThreadStatic] private static LocalDomainEventsManager _events;
        
        static LocalDomainEventsManager Events
        {
            get
            {
                if (_events==null)
                {
                    _events = new LocalDomainEventsManager();
                }
                return _events;
            }
        }
        

        public static IDisposable RegisterHandler<T>(IHandleDomainEvent<T> handler) where T:IDomainEvent
        {
            return Events.RegisterHandler(handler);
        }
        
        public static IDisposable RegisterHandler<T>(Action<T> handler) where T:IDomainEvent
        {
            return Events.RegisterHandler(handler);
        }

        public static void PublishEvent<T>(T evnt) where T:IDomainEvent
        {
            Events.Publish(evnt);
        }

        public static void RemoveHandlers()
        {
            Events.Clear();
        }
    }
}