using System.Collections.Generic;

namespace DomainEvents
{
    public interface ICollectEvents : IAddEvent
    {
        void Enable();
        void Disable();
        void PublishWith(IPublishDomainEvent publisher);
        void Clear();
        IEnumerable<IDomainEvent> GetPendingEvents();
    }
}