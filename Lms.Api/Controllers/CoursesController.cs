using Lms.Application.Contracts.Courses;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService corsService)
        {
            _courseService = corsService;
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateAsync(CreateCourseRequest request, CancellationToken cancellationToken)
        {
            var Course = await  _courseService.CreateAsync(request, cancellationToken);
            return CreatedAtAction("GetById", new { id = Course.Id }, Course);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<CourseDto>>> GetAll(CancellationToken cancellationToken)
        { 
            var courses = await _courseService.GetAllAsync(cancellationToken);
            return Ok(courses);
        }

        [HttpGet("id:guid")]
        public async Task<ActionResult<CourseDto>> GetById (Guid id, CancellationToken cancellationToken)
        {
            var course = await _courseService.GetByIdAsync(id, cancellationToken);
            return course is null
            ? NotFound()
            : Ok(course);
        }

        [HttpPost("{id:guid}/publish")]
        public async Task<ActionResult<CourseDto>> Publish(Guid id, CancellationToken cancellationToken)
        {
            var course = await _courseService.PublishAsync(id, cancellationToken);

            return Ok(course);
        }
    }
}
