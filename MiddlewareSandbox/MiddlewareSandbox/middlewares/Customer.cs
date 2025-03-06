using Microsoft.AspNetCore.Mvc;

namespace MiddlewareSandbox.middlewares
{
    public class Customer : Controller
    {
        private readonly RequestDelegate _next;

        public Customer(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Query.ContainsKey("custom"))
            {
                await context.Response.WriteAsync("You've hit a custom middleware!");
                return;
            }

            await _next(context);
        }
    }
    public static class CustomerExtensions
    {
        public static IApplicationBuilder Customer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Customer>();
        }
    }
}
