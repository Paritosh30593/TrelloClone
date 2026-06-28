using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TC.WebAPI.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                string id = Guid.NewGuid().ToString();

                if (ex.InnerException != null)
                {
                    _logger.LogError(exception: ex.InnerException, message: $"[{id}] {ex.InnerException.GetType().Name}: {ex.InnerException.Message}");
                }
                else
                {
                    _logger.LogError(exception: ex, message: $"[{id}] {ex.GetType().Name}: {ex.Message}");
                }

                httpContext.Response.StatusCode = ex switch
                {
                    ArgumentNullException => StatusCodes.Status400BadRequest,
                    UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError,
                };

                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsJsonAsync(new { error = "An error occurred while processing your request.", traceId = id });
            }
        }
    }
}