using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Common
{
    public interface IHasDomainEvent
    {
        IReadOnlyCollection<DomainEvent> DomainEvents { get; }
        void ClearDomainEvents();
    }
}
