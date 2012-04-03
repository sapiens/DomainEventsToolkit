using System;
using System.Collections.Generic;
using System.Linq;
using DomainEvents.Internals;

namespace DomainEvents.Managers
{
    /// <summary>
    /// Not thread safe. Designed for use in a web context as the domain events manager for a single http request.
    /// </summary>
    public class LocalDomainEventsManager : IDomainEventsManager,IRemoveHandler
    {
        internal List<Subscription> _handlers=new List<Subscription>();
        private IPublishDomainEvent _parent;

        public LocalDomainEventsManager()
        {
            
        }

        internal LocalDomainEventsManager(IPublishDomainEvent parent)
        {
            _parent = parent;
        }

        protected virtual IDisposable RegisterHandlerFor<TEvent>(Action<IDomainEvent> handler) where TEvent:IDomainEvent
        {
            if (handler == null) throw new ArgumentNullException("handler");
            Subscription s;
            s= new Subscription(this,typeof(TEvent),handler);
            _handlers.Add(s);
            return s;
        }

        
        public IDisposable RegisterHandler(IHandleDomainEvent handler)
        {
            return RegisterHandlerFor<IDomainEvent>(handler.Handle);
        }

        public IDisposable RegisterHandler<TEvent>(IHandleDomainEvent<TEvent> handler) where TEvent : IDomainEvent
        {
            if (handler == null) throw new ArgumentNullException("handler");
           
            return RegisterHandlerFor<TEvent>(handler.Handle);
        }

        public IDisposable RegisterHandler<TEvent>(Action<TEvent> handler) where TEvent : IDomainEvent
        {
            if (handler == null) throw new ArgumentNullException("handler");
            Action<IDomainEvent> act = x => { handler((TEvent) x); };
            return RegisterHandlerFor<TEvent>(act);
        }

        internal virtual IEnumerable<Subscription> GetHandlers<TEvent>(TEvent evnt) where TEvent:IDomainEvent
        {
            return _handlers.Where(s => s.CanHandle(evnt)).OrderBy(s => s.IsExactlyFor(evnt) ? 0 : 1).ToArray();
        }

        public void Publish<TEvent>(TEvent evnt) where TEvent : IDomainEvent
        {
            foreach (var s in GetHandlers(evnt))
            {
                s.Handle(evnt);
            }
            if (_parent!=null) _parent.Publish(evnt);
        }

        internal virtual void Remove(Subscription s)
        {
            _handlers.Remove(s);
        }

        void IRemoveHandler.Unsubscribe(Subscription s)
        {
            if (s == null) throw new ArgumentNullException("s");
            Remove(s);
        }

        
    }
}