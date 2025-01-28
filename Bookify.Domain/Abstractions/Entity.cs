using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Abstractions
{
    public abstract class Entity(Guid id, DateTime createdAt)
    {
        private readonly List<IDomainEvent> _domainEvents = [];


        public Guid Id { get; init; } = id;

        public DateTime CreatedAt { get; init; } = createdAt;


        public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents;
        }

        protected void RaiseDomainEvents(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
