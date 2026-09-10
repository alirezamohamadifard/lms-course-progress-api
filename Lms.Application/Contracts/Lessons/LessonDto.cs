using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Application.Contracts.Lessons
{
    public sealed record LessonDto(
        Guid Id,
        Guid CourseId,
        string Title,
        int Order);
}
