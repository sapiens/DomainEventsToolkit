using System;
using System.Collections;
using System.Collections.Generic;
using DomainEvents.Internals;

namespace DomainEvents.Managers
{
    /// <summary>
    /// Thread safe
    /// </summary>
    public class ApplicationDomainEventsManager:LocalDomainEventsManager, IApplicationDomainEventsManager
    {
         object SyncRoot
        {
            get { return ((IList) _handlers).SyncRoot; }
        }

         protected override IDisposable RegisterHandlerFor<TEvent>(Action<IDomainEvent> handler)
         {
             lock(SyncRoot)
             {
                 return base.RegisterHandlerFor<TEvent>(handler);
             }
         }

         internal override IEnumerable<Subscription> GetHandlers<TEvent>(TEvent evnt)
         {
             lock (SyncRoot)
             {
                 return base.GetHandlers(evnt);    
             }
         }

         internal override void Remove(Subscription s)
         {
             lock (SyncRoot)
             {
                 base.Remove(s);
             }             
         }

        public IDomainEventsManager SpawnLocal()
        {
            return new LocalDomainEventsManager(this);
        }
    }
}