using System.Diagnostics;

namespace WebApplication1.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            // Log the request
            Console.WriteLine($"Incoming Request: {context.Request.Method} {context.Request.Path}");

            // Call the next middleware in the pipeline
            await _next(context);

            // Log the response
            stopwatch.Stop();
            Console.WriteLine($"Outgoing Response: {context.Response.StatusCode} in {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
