namespace TC.WebAPI.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string id = Guid.NewGuid().ToString();
                if (ex.InnerException != null)
                {
                    _logger.LogError("[{id}] {ExceptionType}: {Exception}", id, ex.InnerException.GetType().ToString(), ex.InnerException);
                }
                else
                {
                    _logger.LogError("[{id}] {ExceptionType} {Exception}", id, ex.GetType().ToString(), ex);
                }

                throw;
            }
        }
    }
}