using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace MiddlewareSandbox.middlewares
{
    public class Counter : Controller
    {
        private readonly RequestDelegate _next;
        private static int counter = 0;

        public Counter(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/count")
            {
                await context.Response.WriteAsync($"The amount of processed requests is {counter}");
                return;
            }
            counter++;
            await _next(context);
        }
    }

    public static class CounterExtensions
    {
        public static IApplicationBuilder Counter(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Counter>();
        }
    }
}
