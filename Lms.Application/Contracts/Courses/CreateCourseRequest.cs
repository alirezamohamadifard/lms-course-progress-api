using System.ComponentModel.DataAnnotations;

namespace Lms.Application.Contracts.Courses
{
    public sealed class CreateCourseRequest
    {
        [Required]
        [StringLength(200)]
        public string Title { get; init; } = string.Empty;

        [StringLength(2000)]
        public string? Description { get; init; }
    }
}