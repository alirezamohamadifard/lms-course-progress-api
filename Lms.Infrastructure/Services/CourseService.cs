using Lms.Application.Contracts.Courses;
using Lms.Domain.Entity;
using Lms.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Infrastructure.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _dbContext;
        public CourseService(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task<CourseDto> CreateAsync(CreateCourseRequest request, CancellationToken cancellationToken = default)
        {
            var course = new Course(request.Title, request.Description);
            
            _dbContext.Courses.Add(course);
            
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return new CourseDto(
                course.Id,
                course.Title,
                course.Description,
                course.IsPublished,
                course.CreatedAtUtc,
                0);
        }

        public async Task<IReadOnlyList<CourseDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Courses
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAtUtc)
                .Select(x => new CourseDto(
                    x.Id,
                    x.Title,
                    x.Description,
                    x.IsPublished,
                    x.CreatedAtUtc,
                    x.Lessons.Count)).ToListAsync(cancellationToken);
        }

        public async Task<CourseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Courses
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new CourseDto(
                x.Id,
                x.Title,
                x.Description,
                x.IsPublished,
                x.CreatedAtUtc,
                x.Lessons.Count))
            .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
