using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Domain.Entity.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreatedAtUtc { get;  set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
