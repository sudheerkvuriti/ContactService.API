using System.Diagnostics;
using System.Text;
using Microsoft.IO;

namespace ContactService.API.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private readonly RecyclableMemoryStreamManager _streamManager = new();

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            // Read request details
            string requestBody = await ReadRequestBody(context.Request);
            string method = context.Request.Method;
            string path = context.Request.Path;
            string query = context.Request.QueryString.HasValue ? context.Request.QueryString.Value : "";

            // USER INFO (from JWT)
            string? user = context.User?.Identity?.Name ?? "Anonymous";

            // Continue pipeline
            await _next(context);

            stopwatch.Stop();

            // Log details
            _logger.LogInformation(
                "Request Log → User: {User}, Method: {Method}, Path: {Path}, Query: {Query}, Body: {Body}, Status: {Status}, Time: {Time} ms",
                user,
                method,
                path,
                query,
                requestBody,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds
            );
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();

            using var stream = _streamManager.GetStream();
            await request.Body.CopyToAsync(stream);
            string body = Encoding.UTF8.GetString(stream.ToArray());

            request.Body.Position = 0; // Reset for next middleware

            return body;
        }
    }
}
