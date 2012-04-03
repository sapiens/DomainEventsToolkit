using System;

namespace DomainEvents
{
    public interface IHandleDomainEvent
    {
        void Handle(IDomainEvent ev);
    }

    /// <summary>
    /// Marker interface
    /// </summary>
    public interface IHandleDomainEvent<TEvent> : IHandleDomainEvent where TEvent:IDomainEvent
    {
        void Handle(TEvent ev);
    }

    
}