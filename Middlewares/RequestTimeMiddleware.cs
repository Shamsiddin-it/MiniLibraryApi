using System;

namespace WebApi.Middlewares;

public class RequestTimeMiddleware(RequestDelegate next, ILogger<RequestTimeMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<RequestTimeMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation(
            "Incoming request: {Method} {Path}",
            context.Request.Method,
            context.Request.Path
        );
        var start = DateTime.Now;
        try
        {
            await _next(context);
        }
        catch
        {
            _logger.LogError("The request was not successfull");
        }
        var end = DateTime.Now;
        _logger.LogInformation("The request finished!");

        Console.WriteLine($"Request took {(end - start).TotalMilliseconds} ms");
    }
}
