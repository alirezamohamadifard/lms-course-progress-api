using Lms.Domain.Entity.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Domain.Entity
{
    public class Lesson : BaseEntity
    {
        public Guid CourseId { get; private set; }
        public string Title { get; private set; } = null!;
        public int Order { get; private set; }
        private Lesson()
        {
        }

        public Lesson(Guid courseId, string title, int order)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException("Lesson Title is required", nameof(title));
            CourseId = courseId;
            Title = title;
            Order = order;
            CreatedAtUtc = DateTime.UtcNow;
        }
    }
}
