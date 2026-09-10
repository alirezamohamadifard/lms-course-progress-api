
namespace Lms.Application.Contracts.Courses
{
    public sealed record CourseDto(
        Guid Id,
        string Title,
        string? Description,
        bool IsPublished,
        DateTime CreatedAtUtc,
        int LessonCount);
}