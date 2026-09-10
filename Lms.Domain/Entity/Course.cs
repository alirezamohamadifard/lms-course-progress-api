using Lms.Domain.Entity.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lms.Domain.Entity
{
    public class Course : BaseEntity
    {
        private readonly List<Lesson> _lessons = [];
        public string Title { get; private set; } = null!;
        public string? Description { get; private set; }
        public bool IsPublished { get; private set; }
        public ICollection<Lesson> Lessons => _lessons.AsReadOnly();
        private Course()
        {
        }
        public Course(string title, string? description)
        {
            UpdateDetails(title, description);
            CreatedAtUtc = DateTime.UtcNow;
        }

        private void UpdateDetails(string title, string? description)
        {

            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException("Course Title is required", nameof(title));
            Title = title.Trim();
            Description = description?.Trim();
        }
        private void AddLesson(string title)
        {
            var nextorder = _lessons.Count + 1;
            var lesson  =  new Lesson(Id, title, nextorder);
            _lessons.Add(lesson);
        }
        public void Publish()
        {
            if (Lessons.Count == 0)
            {
                throw new InvalidOperationException("A Course must have at least one Lesson before publishing.");
            }
            IsPublished = true;
        }
    }
}
