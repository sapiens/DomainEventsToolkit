namespace DomainEvents
{
    /// <summary>
    /// Base class for a domain event
    /// </summary>
    /// <typeparam name="T">event data type</typeparam>
    public class DomainEventBase<T>:IDomainEvent
    {
        public DomainEventBase(T data)
        {
            Data = data;
        }
        public T Data { get; protected set; }
     
    }
}