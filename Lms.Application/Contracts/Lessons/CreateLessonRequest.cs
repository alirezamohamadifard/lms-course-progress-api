using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lms.Application.Contracts.Lessons
{
    public sealed class CreateLessonRequest
    {
        [Required]
        [StringLength(200)]
        public string Title { get; init; } = string.Empty;
    }
}
