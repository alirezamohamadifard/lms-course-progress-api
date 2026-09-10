namespace Lms.Application.Contracts.Courses
{
    public sealed record CreateCourseRequest(string Title, string? Description);
}