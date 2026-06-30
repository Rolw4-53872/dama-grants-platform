using System.Net;
using System.Text.Json;

namespace DamaGrant.API.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();

        var response = new
        {
            status = context.Response.StatusCode,
            message = "An error occurred while processing your request",
            detail = exception.Message,
            timestamp = DateTime.UtcNow
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}
