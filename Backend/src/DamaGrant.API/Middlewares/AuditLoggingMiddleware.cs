namespace DamaGrant.API.Middlewares;

public class AuditLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuditLoggingMiddleware> _logger;

    public AuditLoggingMiddleware(RequestDelegate next, ILogger<AuditLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        var userId = context.User.FindFirst("sub")?.Value ?? "Anonymous";
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();
        var method = request.Method;
        var path = request.Path;

        _logger.LogInformation(
            "HTTP Request: Method={Method}, Path={Path}, UserId={UserId}, IpAddress={IpAddress}",
            method, path, userId, ipAddress);

        await _next(context);

        var response = context.Response;
        _logger.LogInformation(
            "HTTP Response: Method={Method}, Path={Path}, StatusCode={StatusCode}",
            method, path, response.StatusCode);
    }
}
