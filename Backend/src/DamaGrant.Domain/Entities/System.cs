namespace DamaGrant.Domain.Entities;

public class Department : AuditableEntity
{
    public string DepartmentName { get; set; } = null!;
    public string? Description { get; set; }
    public string? HeadName { get; set; }
    public string? HeadEmail { get; set; }
    public string? HeadPhone { get; set; }
    public int? ParentDepartmentId { get; set; }
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }
    public string? LocationCode { get; set; }

    public Department? ParentDepartment { get; set; }
    public ICollection<Department> SubDepartments { get; set; } = [];
}

public class Settings : BaseEntity
{
    public string SettingKey { get; set; } = null!;
    public string SettingValue { get; set; } = null!;
    public string SettingType { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsEncrypted { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
}

public class AuditLog : BaseEntity
{
    public int? UserId { get; set; }
    public AuditActionType ActionType { get; set; }
    public string EntityType { get; set; } = null!;
    public int EntityId { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? AffectedColumns { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? Remarks { get; set; }

    public User? User { get; set; }
}

public class ActivityLog : BaseEntity
{
    public int? UserId { get; set; }
    public string ActivityType { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string EntityType { get; set; } = null!;
    public int? EntityId { get; set; }
    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;
    public string? IpAddress { get; set; }
    public string? Location { get; set; }
    public int? DurationInMilliseconds { get; set; }

    public User? User { get; set; }
}

public class FileStorage : AuditableEntity
{
    public string FileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public long FileSizeInBytes { get; set; }
    public string FileContentType { get; set; } = null!;
    public string FileExtension { get; set; } = null!;
    public DateTime UploadedAt { get; set; }
    public int? UploadedBy { get; set; }
    public string? StorageLocation { get; set; }
    public string? BlobUrl { get; set; }
    public string EntityType { get; set; } = null!;
    public int? EntityId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public int? DeletedBy { get; set; }
    public long? DownloadCount { get; set; }
    public DateTime? LastDownloadedAt { get; set; }

    public User? UploadedByUser { get; set; }
}

public class Lookup : AuditableEntity
{
    public string LookupName { get; set; } = null!;
    public string LookupType { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }
    public bool IsSystem { get; set; }

    public ICollection<LookupValue> Values { get; set; } = [];
}

public class LookupValue : AuditableEntity
{
    public int LookupId { get; set; }
    public string ValueCode { get; set; } = null!;
    public string ValueText { get; set; } = null!;
    public string? ValueDescription { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public string? ParentValueCode { get; set; }
    public decimal? NumericValue { get; set; }

    public Lookup Lookup { get; set; } = null!;
}
