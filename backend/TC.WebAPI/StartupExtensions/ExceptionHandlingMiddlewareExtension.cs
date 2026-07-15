using Microsoft.AspNetCore.Builder;
using TC.WebAPI.Middleware;

namespace TC.WebAPI.StartupExtensions
{
    public static class ExceptionHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this WebApplication app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}