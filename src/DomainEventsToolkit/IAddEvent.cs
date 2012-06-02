namespace DomainEvents
{
    public interface IAddEvent
    {
        void Add(IDomainEvent ev);
    }
}