using Lms.Application.Contracts.Enrollments;
using Lms.Domain.Entity;
using Lms.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Infrastructure.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly AppDbContext _dbContext;
        public EnrollmentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<EnrollmentDto> EnrollAsync(Guid userId, Guid courseId, CancellationToken cancellationToken = default)
        {
            var course = await _dbContext.Courses
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == courseId, cancellationToken);

            if (course is null )
                throw new KeyNotFoundException("Course was not found");

            if (!course.IsPublished)
                throw new InvalidOperationException("Only published courses can be enrolled in.");

            var userexist = await _dbContext.Users
                .AnyAsync(x => x.Id ==  userId, cancellationToken);
            
            if (!userexist)
                throw new KeyNotFoundException("user not found");

            var enrollmentexist = await _dbContext.Enrollments
                .AnyAsync(x => x.UserId == userId && x.CourseId == courseId, cancellationToken);

            if (enrollmentexist)
                throw new InvalidOperationException("You are already enrolled in this course.");

            var enrollment = new Enrollment(userId, courseId);

            _dbContext.Enrollments.Add(enrollment);

            await _dbContext.SaveChangesAsync();

            return new EnrollmentDto(
            enrollment.Id,
            course.Id,
            course.Title,
            enrollment.CreatedAtUtc);
        }

        public async Task<IReadOnlyList<EnrollmentDto>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await (
            from enrollment in _dbContext.Enrollments.AsNoTracking()
            join course in _dbContext.Courses.AsNoTracking()
                on enrollment.CourseId equals course.Id
            where enrollment.UserId == userId
            orderby enrollment.CreatedAtUtc descending
            select new EnrollmentDto(
                enrollment.Id,
                course.Id,
                course.Title,
                enrollment.CreatedAtUtc)
        ).ToListAsync(cancellationToken);
        }
    }
}
