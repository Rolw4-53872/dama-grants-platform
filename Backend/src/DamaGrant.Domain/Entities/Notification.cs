namespace DamaGrant.Domain.Entities;

public class Notification : AuditableEntity
{
    public int UserId { get; set; }
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
    public NotificationType NotificationType { get; set; }
    public int? RelatedEntityId { get; set; }
    public string? RelatedEntityType { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ReadAt { get; set; }
    public DateTime? SentAt { get; set; }
    public bool IsSent { get; set; }
    public string? ActionUrl { get; set; }
    public bool HasAction { get; set; }

    public User User { get; set; } = null!;
    public ICollection<NotificationRecipient> Recipients { get; set; } = [];
}

public class NotificationTemplate : AuditableEntity
{
    public string TemplateName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public NotificationType NotificationType { get; set; }
    public string EmailSubject { get; set; } = null!;
    public string EmailBodyTemplate { get; set; } = null!;
    public string InAppMessageTemplate { get; set; } = null!;
    public string? SMSMessageTemplate { get; set; }
    public bool IsActive { get; set; } = true;
    public bool SendEmail { get; set; } = true;
    public bool SendInApp { get; set; } = true;
    public bool SendSMS { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class NotificationRecipient : BaseEntity
{
    public int NotificationId { get; set; }
    public int RecipientUserId { get; set; }
    public bool IsRead { get; set; }
    public DateTime? ReadAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public Notification Notification { get; set; } = null!;
    public User Recipient { get; set; } = null!;
}

public class EmailQueue : AuditableEntity
{
    public string RecipientEmail { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
    public bool IsHtml { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? SentAt { get; set; }
    public bool IsSent { get; set; }
    public int? RetryCount { get; set; }
    public DateTime? LastRetryAt { get; set; }
    public string? ErrorMessage { get; set; }
    public int? RelatedNotificationId { get; set; }

    public Notification? RelatedNotification { get; set; }
}
