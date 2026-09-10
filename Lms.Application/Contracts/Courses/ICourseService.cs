using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Application.Contracts.Courses
{
    public interface ICourseService
    {
        Task<CourseDto> CreateAsync(
        CreateCourseRequest request,
        CancellationToken cancellationToken = default);

        Task<IReadOnlyList<CourseDto>> GetAllAsync(
           CancellationToken cancellationToken = default);

        Task<CourseDto?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);
    }
}

