using Lms.Application.Contracts.Lessons;
using Lms.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Infrastructure.Services
{
    public class LessonService : ILessonService
    {
        private readonly AppDbContext _dbContext;
        public LessonService(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task<LessonDto> CreateAsync(Guid courseId, CreateLessonRequest request, CancellationToken cancellationToken = default)
        {
            var course = await _dbContext
                .Courses
                .Include(x => x.Lessons)
                .SingleOrDefaultAsync(x => x.Id == courseId, cancellationToken);

            if (course is null)
            {
                throw new KeyNotFoundException("Course was not found.");
            }

            course.AddLesson(request.Title);

            var lesson = course.Lessons
                .MaxBy(x => x.Order)!;

            _dbContext.Lessons.Add(lesson);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new LessonDto(
            lesson.Id,
            lesson.CourseId,
            lesson.Title,
            lesson.Order);
        }

        public async Task<IReadOnlyList<LessonDto>> GetByCourseIdAsync(Guid courseId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Lessons
             .AsNoTracking()
             .Where(x => x.CourseId == courseId)
             .OrderBy(x => x.Order)
             .Select(x => new LessonDto(
                 x.Id,
                 x.CourseId,
                 x.Title,
                 x.Order))
             .ToListAsync(cancellationToken);
        }
    }
}
