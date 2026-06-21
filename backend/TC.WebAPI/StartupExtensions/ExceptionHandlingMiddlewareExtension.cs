using TC.WebAPI.Middleware;

namespace TC.WebAPI.StartupExtensions
{
    public static class ExceptionHandlingMiddlewareExtension
    {
        public static void UseExceptionHandlingMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}