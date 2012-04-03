namespace DomainEvents.Internals
{
    internal interface IRemoveHandler
    {
        void Unsubscribe(Subscription s);
    }
}