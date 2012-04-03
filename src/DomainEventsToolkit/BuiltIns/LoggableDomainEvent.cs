using System;

namespace DomainEvents.BuiltIns
{
    /// <summary>
    /// Base class for domain events implementing ILoggableDomainEvent
    /// </summary>
    /// <typeparam name="T">Event Data Type</typeparam>
    public class LoggableDomainEvent<T>:DomainEventBase<T>,ILoggableDomainEvent
    {
        public LoggableDomainEvent(T data) : base(data)
        {
            EventName = GetType().Name;
            Time = DateTime.Now;
        }

        public string EventName { get; private set; }

       
        public string AdditionalMessage { get; set; }

        public DateTime Time { get; private set; }

        public int LogLevel { get; set; }
    }
   
}