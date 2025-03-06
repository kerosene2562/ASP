using Microsoft.AspNetCore.Mvc;

namespace MiddlewareSandbox.middlewares
{
    public class Logger : Controller
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<Logger> _logger;

        public Logger(RequestDelegate next, ILogger<Logger> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            string requestMethod = context.Request.Method;
            string requestPath = context.Request.Path;

            _logger.LogInformation("Request received: {Method} {Path}", requestMethod, requestPath);

            await _next(context);
        }
    }
    public static class LoggerExtensions
    {
        public static IApplicationBuilder Logger(this IApplicationBuilder app)
        {
            return app.UseMiddleware<Logger>();
        }
    }
}
