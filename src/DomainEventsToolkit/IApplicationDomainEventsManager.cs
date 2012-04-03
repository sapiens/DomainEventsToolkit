namespace DomainEvents
{
    public interface IApplicationDomainEventsManager:IDomainEventsManager
    {
        /// <summary>
        /// Creates a local, not thread-safe domain events manager. 
        /// Designed for web usage as the domain events manager for a single http request.
        /// Every event published by the local manager will be also published by the application manager
        /// </summary>
        /// <returns></returns>
        IDomainEventsManager SpawnLocal();
    }
}