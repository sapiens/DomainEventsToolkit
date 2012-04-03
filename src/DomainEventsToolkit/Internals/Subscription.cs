using System;

namespace DomainEvents.Internals
{
    internal class Subscription:IDisposable,IEquatable<Subscription>
    {
        private IRemoveHandler _manager;
        private Type _event;
        private Action<IDomainEvent> _handler;

        public Subscription(IRemoveHandler d,Type evnt,Action<IDomainEvent> handler)
        {
            if (d == null) throw new ArgumentNullException("d");
        
            if (handler == null) throw new ArgumentNullException("handler");
            _manager = d;
            _event = evnt;
            _handler = handler;
        }

        public bool CanHandle(IDomainEvent evnt)
        {
            var tp = evnt.GetType();
            return _event.IsAssignableFrom(tp);
        }

        public bool IsExactlyFor(IDomainEvent evnt)
        {
            return _event.Equals(evnt.GetType());
        }

        public void Handle(IDomainEvent evnt)
        {
            _handler(evnt);
        }

        public void Dispose()
        {
            _manager.Unsubscribe(this);
        }

        public bool Equals(Subscription other)
        {
            if (other == null) return false;
            return other._event.Equals(_event) && other._handler == _handler;
        }

        public override bool Equals(object obj)
        {
            return Equals((Subscription)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return _event.GetHashCode()*29 + _handler.GetHashCode();
            }
            
        }
    }
}