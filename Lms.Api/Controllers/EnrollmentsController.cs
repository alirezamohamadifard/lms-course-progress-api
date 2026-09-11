using Lms.Application.Contracts.Enrollments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Lms.Api.Controllers
{
    [Route("api")]
    [Authorize]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPost("Course/{courseId:guid}/enrollments")]
        public async Task<ActionResult<EnrollmentDto>> Enroll(Guid courseId, CancellationToken cancellationToken)
        {
            if (!TryGetCurrentUserId(out var userId))
                return Unauthorized();

            var enroll = await _enrollmentService.EnrollAsync(userId, courseId, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, enroll);
        }

        [HttpGet("me/enrollments")]
        public async Task<ActionResult<IReadOnlyCollection<EnrollmentDto>>> GetMyEnrollments(CancellationToken cancellationToken)
        {
            if (!TryGetCurrentUserId(out var userId))
                return Unauthorized();

            var enrollments = await _enrollmentService.GetByUserIdAsync(userId, cancellationToken);

            return Ok(enrollments);
        }

        private bool TryGetCurrentUserId(out Guid userId)
        {
            var value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            return Guid.TryParse(value, out userId);
        }
    }
}
