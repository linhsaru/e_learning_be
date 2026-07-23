using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Common
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; protected set; } = default!;
    }
}
