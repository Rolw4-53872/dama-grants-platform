namespace DamaGrant.Infrastructure.External;

public interface INotificationService
{
    Task SendNotificationAsync(int userId, string title, string message, string type);
    Task SendEmailNotificationAsync(string email, string subject, string message);
}

public class NotificationService : INotificationService
{
    private readonly IEmailService _emailService;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(IEmailService emailService, ILogger<NotificationService> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task SendNotificationAsync(int userId, string title, string message, string type)
    {
        _logger.LogInformation($"Notification sent to user {userId}: {title}");
        await Task.CompletedTask;
    }

    public async Task SendEmailNotificationAsync(string email, string subject, string message)
    {
        await _emailService.SendEmailAsync(email, subject, message);
    }
}
