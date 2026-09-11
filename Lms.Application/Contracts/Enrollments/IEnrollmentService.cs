using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Application.Contracts.Enrollments
{

    public interface IEnrollmentService
    {
        Task<EnrollmentDto> EnrollAsync(
            Guid userId,
            Guid courseId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<EnrollmentDto>> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
    }
}
