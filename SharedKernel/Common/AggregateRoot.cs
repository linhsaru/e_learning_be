using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Common
{
    public abstract class AggregateRoot<TId> : AuditableEntity<TId>, IAggregateRoot, IHasDomainEvent
    {
        private readonly List<DomainEvent> _domainEvents = [];
        protected IReadOnlyCollection<DomainEvent> DomainEvents
            => _domainEvents.AsReadOnly();

        IReadOnlyCollection<DomainEvent> IHasDomainEvent.DomainEvents => DomainEvents;

        protected void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
