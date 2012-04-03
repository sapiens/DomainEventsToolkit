using System.Text;
using DomainEvents;
using DomainEvents.Managers;
using Xunit;
using System;
using System.Diagnostics;

namespace Tests
{
    public class ApplicationDomainEventsTests
    {
        private Stopwatch _t = new Stopwatch();
        private IApplicationDomainEventsManager _app;

        public ApplicationDomainEventsTests()
        {
            _app = new ApplicationDomainEventsManager();
        }

        [Fact]
        public void register_handler_ok()
        {
            Assert.ThrowsDelegate act = () =>
                             {
                                 _app.RegisterHandler<MyEvent>(ev => { });
                             };
            
            Assert.DoesNotThrow(act);  
        }

        [Fact]
        public void cant_register_null_handler()
        {
            Assert.Throws<ArgumentNullException>(() => _app.RegisterHandler<MyEvent>((Action<IDomainEvent>)null));
        }

        [Fact]
        public void publish_event_is_handled()
        {
            var handled = false;
            _app.RegisterHandler<MyEvent>(x=> handled=true);
            _app.Publish(new MyEvent());
            Assert.True(handled);
        }

        [Fact]
        public void unsubscribe_event_handler()
        {
            var handled = false;
            using(_app.RegisterHandler<MyEvent>(x => handled = !handled))
            {
                _app.Publish(new MyEvent());
                Assert.True(handled);                
            }
            
            _app.Publish(new MyEvent());
            Assert.True(handled);
        }

        [Fact]
        public void inherited_events_handled_from_specific_to_base()
        {
            var i = 0;
            _app.RegisterHandler<IDomainEvent>(x => i++); 
            _app.RegisterHandler<MyEvent>(x => i = 2);                    
            _app.Publish(new MyEvent());
            Assert.Equal(3,i);
        }

        [Fact]
        public void only_specific_event_is_handled()
        {
            var i = 0;
            _app.RegisterHandler<MyEvent2>(x => i = 2);
            _app.Publish(new MyEvent());
            Assert.Equal(0,i);
        }

        [Fact]
        public void usage_test()
        {
            var sb = new StringBuilder();
            var glb=_app.RegisterHandler(new MySimpleHandler(sb));
            _app.Publish(new AnEvent());
            Assert.Equal("From generic event",sb.ToString());
            sb.Clear();
            using(_app.RegisterHandler(new MyEventHandler(sb)))
            {
                _app.Publish(new MyEvent(){Text = "myevent"});
                Assert.Equal("Hello myeventFrom generic event", sb.ToString());
                sb.Clear();
                glb.Dispose();
                _app.Publish(new MyEvent2(){Text = "childevent"});
                Assert.Equal("Hello childevent",sb.ToString());
                sb.Clear();
                _app.Publish(new AnEvent());
                Assert.Equal("",sb.ToString());
            }                       
        }

        [Fact]
        public void local_manager_also_publish_to_app_manager()
        {
            var sb = new StringBuilder();
            _app.RegisterHandler(new MySimpleHandler(sb));
            var local = _app.SpawnLocal();
            local.RegisterHandler<MyEvent>(new MyEventHandler(sb));
            local.Publish(new MyEvent(){Text = "myevent"});
            Assert.Equal("Hello myeventFrom generic event",sb.ToString());
        }

        private void Write(string format, params object[] param)
        {
            Console.WriteLine(format, param);
        }
    }
}