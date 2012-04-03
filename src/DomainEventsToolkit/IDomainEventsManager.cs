using System;

namespace DomainEvents
{
    public interface IDomainEventsManager:IPublishDomainEvent
    {
        /// <summary>
        /// Register handler for a domain event
        /// </summary>
        /// <typeparam name="TEvent">Event type</typeparam>
        /// <param name="handler"></param>
        /// <returns></returns>
        IDisposable RegisterHandler<TEvent>(Action<TEvent> handler) where TEvent:IDomainEvent;
        /// <summary>
        /// Register handler for a domain event
        /// </summary>
        /// <typeparam name="TEvent">Event type</typeparam>
        /// <param name="handler"></param>
        /// <returns></returns>
        IDisposable RegisterHandler<TEvent>(IHandleDomainEvent<TEvent> handler) where TEvent:IDomainEvent;

        /// <summary>
        /// Register a handler for any domain event
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        IDisposable RegisterHandler(IHandleDomainEvent handler);
    }
}