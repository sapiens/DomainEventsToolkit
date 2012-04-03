namespace DomainEvents
{
    /// <summary>
    /// Base class for a domain event handler
    /// </summary>
    public abstract class DomainEventHandlerBase<TEvent>:DomainEventHandlerBase,IHandleDomainEvent<TEvent> where TEvent:IDomainEvent
    {
        public abstract void Handle(TEvent ev);
        public override void Handle(IDomainEvent ev)
        {
            Handle((TEvent)ev);
        }
    }

    public abstract class DomainEventHandlerBase:IHandleDomainEvent
    {
        public abstract void Handle(IDomainEvent ev);
    }
}