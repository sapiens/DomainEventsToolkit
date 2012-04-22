using System;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using DomainEvents;
using DomainEvents.Managers;

namespace Tests
{
    public class Setup
    {
        public static IDomainEventsManager LocalManager
        {
            get
            {
                return new LocalDomainEventsManager();
            }
        }
    }

    class AnEvent:IDomainEvent
    {
        
    }

    public class MyEvent:IDomainEvent
    {
        public string Text { get; set; }
    }


    public class MyEvent2:MyEvent
    {
        
    }

    class MySimpleHandler:DomainEventHandlerBase
    {
        private StringBuilder _s;

        public MySimpleHandler(StringBuilder s)
        {
            _s = s;            
        }
        public override void Handle(IDomainEvent ev)
        {
           _s.Append("From generic event");
        }
    }

    public class MyEventHandler:DomainEventHandlerBase<MyEvent>
    {
        private StringBuilder _s;

        public MyEventHandler(StringBuilder s)
        {
            _s = s;
        }
        public override void Handle(MyEvent ev)
        {
            _s.Append("Hello " + ev.Text);
        }
    }
}