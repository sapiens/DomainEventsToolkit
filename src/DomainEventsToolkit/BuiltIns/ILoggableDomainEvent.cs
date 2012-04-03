using System;

namespace DomainEvents.BuiltIns
{
    public interface ILoggableDomainEvent : IDomainEvent
    {
        string EventName { get; }
        string AdditionalMessage { get;  }
        DateTime Time { get; }
        int LogLevel { get; }
    }
}