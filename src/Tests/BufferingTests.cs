using DomainEvents;
using Xunit;
using System;
using System.Diagnostics;

namespace Tests
{
    public class BufferingTests
    {
        private Stopwatch _t = new Stopwatch();
        private IDomainEventsManager _manager;

        private bool _handled=false;
        public BufferingTests()
        {
            _manager = Setup.LocalManager;
            _manager.RegisterHandler<MyEvent2>(ev => { 
                _handled = true;
                Write(ev.GetType().ToString());
            });
        }

        [Fact]
        public void if_buffered_events_are_not_published_immediately()
        {
            _manager.BeginBuffering();
            _manager.Publish(new MyEvent2());
            Assert.False(_handled);
        }

        [Fact]
        public void end_buffering_risees_all_buffered_events()
        {
            string rez = string.Empty;
            _manager.BeginBuffering();
            _manager.RegisterHandler<MyEvent>(ev => rez = ev.Text);
            _manager.Publish(new MyEvent2());
            _manager.Publish(new MyEvent(){Text = "text2"});
            Assert.True(string.IsNullOrEmpty(rez));
            _manager.EndBuffering();
            Assert.True(_handled);
            Assert.Equal("text2",rez);
        }

        [Fact]
        public void disposing_buffersub_triggers_end_buffer()
        {
            using(_manager.BeginBuffering())
            {
               _manager.Publish(new MyEvent2()); 
            }
            Assert.True(_handled);
        }

        [Fact]
        public void if_buffering_beginbuffer_returns_same_sub()
        {
            var sub = _manager.BeginBuffering();
            var sub2 = _manager.BeginBuffering();
            Assert.Same(sub,sub2);
        }

        [Fact]
        public void ending_when_not_started_throws()
        {
            Assert.Throws<InvalidOperationException>(() => _manager.EndBuffering());
        }
        private void Write(string format, params object[] param)
        {
            Console.WriteLine(format, param);
        }
    }
}