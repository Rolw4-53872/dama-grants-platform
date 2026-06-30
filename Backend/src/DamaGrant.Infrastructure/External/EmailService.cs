using System.Net;
using System.Net.Mail;

namespace DamaGrant.Infrastructure.External;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body, bool isHtml = false);
    Task SendEmailAsync(List<string> to, string subject, string body, bool isHtml = false);
}

public class EmailService : IEmailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _senderEmail;
    private readonly string _senderPassword;
    private readonly bool _enableSSL;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        var emailSettings = configuration.GetSection("EmailSettings");
        _smtpServer = emailSettings["SmtpServer"] ?? "smtp.gmail.com";
        _smtpPort = int.Parse(emailSettings["SmtpPort"] ?? "587");
        _senderEmail = emailSettings["SenderEmail"] ?? "";
        _senderPassword = emailSettings["SenderPassword"] ?? "";
        _enableSSL = bool.Parse(emailSettings["EnableSSL"] ?? "true");
        _logger = logger;
    }

    public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = false)
    {
        await SendEmailAsync(new List<string> { to }, subject, body, isHtml);
    }

    public async Task SendEmailAsync(List<string> to, string subject, string body, bool isHtml = false)
    {
        try
        {
            using (var client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.EnableSsl = _enableSSL;
                client.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(_senderEmail);
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = isHtml;

                    foreach (var recipient in to)
                    {
                        message.To.Add(recipient);
                    }

                    await client.SendMailAsync(message);
                    _logger.LogInformation($"Email sent successfully to: {string.Join(",", to)}");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to send email to: {string.Join(",", to)}");
            throw;
        }
    }
}
