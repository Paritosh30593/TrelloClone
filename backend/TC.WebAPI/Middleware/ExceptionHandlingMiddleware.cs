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
                    _logger.LogError("[{id}] {ExceptionType}: {Exception}", id, ex.InnerException.GetType().Name, ex.InnerException.Message);
                }
                else
                {
                    _logger.LogError("[{id}] {ExceptionType}: {Exception}", id, ex.GetType().Name, ex.Message);
                }
                throw;
            }
        }
    }
}