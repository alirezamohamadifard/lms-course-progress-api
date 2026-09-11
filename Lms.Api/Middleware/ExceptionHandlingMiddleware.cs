namespace Lms.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (KeyNotFoundException exception)
            {
                await WriteErrorAsync(
                    context,
                    StatusCodes.Status404NotFound, 
                    exception.Message);
            }
            catch (ArgumentException exception)
            { 
                await WriteErrorAsync(
                    context,
                    StatusCodes.Status400BadRequest,
                    exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                await WriteErrorAsync(
                    context,
                    StatusCodes.Status401Unauthorized,
                    exception.Message);
            }
            catch (InvalidOperationException exception)
            {
                await WriteErrorAsync(
                    context,
                    StatusCodes.Status409Conflict,
                    exception.Message);
            }
            catch (Exception exception) 
            {
                _logger.LogError(exception, "An Unexpected Error Occured");
                await WriteErrorAsync(
                    context,
                    StatusCodes.Status500InternalServerError,
                   "An unexpected error occurred.");
            }
        }

        private async Task WriteErrorAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                statusCode,
                message
            });
        }
    }
}
