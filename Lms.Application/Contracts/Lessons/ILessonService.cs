using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Application.Contracts.Lessons
{
    public interface ILessonService
    {
        Task<LessonDto> CreateAsync(
        Guid courseId,
        CreateLessonRequest request,
        CancellationToken cancellationToken = default);

        Task<IReadOnlyList<LessonDto>> GetByCourseIdAsync(
            Guid courseId,
            CancellationToken cancellationToken = default);
    }
}
