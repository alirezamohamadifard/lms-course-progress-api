using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Application.Contracts.Enrollments
{
    public sealed record EnrollmentDto(
    Guid Id,
    Guid CourseId,
    string CourseTitle,
    DateTime EnrolledAtUtc);
}
