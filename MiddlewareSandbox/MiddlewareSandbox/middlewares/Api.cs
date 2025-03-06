using Microsoft.AspNetCore.Mvc;

namespace MiddlewareSandbox.middlewares
{
    public class Api : Controller
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeaderName = "X-API-KEY";
        private const string Key = "apikey";

        public Api(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey) || extractedApiKey != Key)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Invalid API key.");
                return;
            }

            await _next(context);
        }
    }
    public static class ApiExtensions
    {
        public static IApplicationBuilder Api(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Api>();
        }
    }
}