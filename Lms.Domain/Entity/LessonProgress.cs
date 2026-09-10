using Lms.Domain.Entity.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Domain.Entity
{
    public class LessonProgress : BaseEntity
    {
        public Guid EnrollmentId { get; private set; }
        public Guid LessonId { get; private set; }
        public bool IsCompleted { get; private set; }
        public DateTime? CompletedAtUtc { get; private set; }
        private LessonProgress()
        {
            
        }
        public LessonProgress(Guid enrollmentId, Guid lessonId)
        {
            EnrollmentId = enrollmentId;
            LessonId = lessonId;
            CreatedAtUtc = DateTime.UtcNow;
        }
        public void MarkAsComplete()
        {
            IsCompleted = true;
            CompletedAtUtc = DateTime.UtcNow;
        }

    }
}
