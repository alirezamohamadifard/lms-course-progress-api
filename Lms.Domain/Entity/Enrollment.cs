using Lms.Domain.Entity.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Domain.Entity
{
    public class Enrollment : BaseEntity
    {
        public Guid UserId { get; private set; }
        public Guid CourseId { get; private set; }
        private Enrollment()
        {
        }
        public Enrollment(Guid userId, Guid courseId)
        {
            UserId = userId;
            CourseId = courseId;
            CreatedAtUtc = DateTime.UtcNow;
        }
    }
}
