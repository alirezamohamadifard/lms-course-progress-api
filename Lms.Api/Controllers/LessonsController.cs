using Lms.Application.Contracts.Lessons;
using Lms.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Lms.Api.Controllers
{
    [Route("api/courses/{courseId:guid}/lessons")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<ActionResult<LessonDto>> Create(Guid courseId, CreateLessonRequest request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonService.CreateAsync(courseId, request, cancellationToken);

            return CreatedAtAction(nameof(GetByCourseId), new { courseId }, lesson);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<LessonDto>>> GetByCourseId(Guid courseId, CancellationToken cancellationToken)
        {
            var lessons = await _lessonService.GetByCourseIdAsync(
            courseId,
            cancellationToken);

            return Ok(lessons);
        }
    }
}
