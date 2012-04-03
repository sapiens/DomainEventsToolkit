namespace DomainEvents
{
    public interface IPublishDomainEvent
    {
        /// <summary>
        /// Publish domain event 
        /// </summary>
        /// <typeparam name="TEvent">Domain Event</typeparam>
        /// <param name="evnt">Instance of domain event</param>
        void Publish<TEvent>(TEvent evnt) where TEvent:IDomainEvent;
    }
}