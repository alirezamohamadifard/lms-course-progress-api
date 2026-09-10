using Lms.Application.Contracts.Lessons;
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
        public async Task<ActionResult<LessonDto>> Create(Guid courseId, CreateLessonRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var lesson = await _lessonService.CreateAsync(courseId, request, cancellationToken);

                return CreatedAtAction(nameof(GetByCourseId), new { courseId }, lesson);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    message = "Course was not found."
                });
            }
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
