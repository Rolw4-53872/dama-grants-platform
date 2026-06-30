namespace DamaGrant.Domain.Entities;

public class User : AuditableEntity
{
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public UserStatus Status { get; set; } = UserStatus.PendingVerification;
    public bool EmailConfirmed { get; set; }
    public DateTime? EmailConfirmedAt { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public DateTime? PhoneNumberConfirmedAt { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTime? LockoutEndDate { get; set; }
    public int FailedLoginAttempts { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public string? ProfileImageUrl { get; set; }
    public int? AssociationId { get; set; }

    public Association? Association { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = [];
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    public ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = [];
    public ICollection<LoginHistory> LoginHistories { get; set; } = [];
}

public class Role : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = [];
    public ICollection<RolePermission> RolePermissions { get; set; } = [];
}

public class Permission : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Resource { get; set; } = null!;
    public string Action { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ICollection<RolePermission> RolePermissions { get; set; } = [];
}

public class UserRole : BaseEntity
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    public int? AssignedBy { get; set; }

    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
}

public class RolePermission : BaseEntity
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    public Role Role { get; set; } = null!;
    public Permission Permission { get; set; } = null!;
}

public class RefreshToken : BaseEntity
{
    public int UserId { get; set; }
    public string Token { get; set; } = null!;
    public string JwtId { get; set; } = null!;
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedByIp { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string? RevokedByIp { get; set; }

    public User User { get; set; } = null!;
}

public class PasswordResetToken : BaseEntity
{
    public int UserId { get; set; }
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UsedAt { get; set; }

    public User User { get; set; } = null!;
}

public class EmailVerificationToken : BaseEntity
{
    public int UserId { get; set; }
    public string Token { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UsedAt { get; set; }

    public User User { get; set; } = null!;
}

public class LoginHistory : BaseEntity
{
    public int UserId { get; set; }
    public DateTime LoginAt { get; set; } = DateTime.UtcNow;
    public DateTime? LogoutAt { get; set; }
    public string IpAddress { get; set; } = null!;
    public string UserAgent { get; set; } = null!;
    public string? DeviceName { get; set; }
    public bool IsSuccessful { get; set; }
    public string? FailureReason { get; set; }

    public User User { get; set; } = null!;
}
