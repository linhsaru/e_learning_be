using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Common
{
    public abstract record DomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOnUtc { get; init; } = DateTime.UtcNow;
    }
}
