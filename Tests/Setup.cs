using System;
using System.Text;
using DomainEvents;

namespace Tests
{
    public class Setup
    {
         
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