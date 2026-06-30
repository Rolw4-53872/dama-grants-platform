using DamaGrant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DamaGrant.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();

        builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.HasIndex(u => u.PhoneNumber).IsUnique();

        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(500);

        builder.Property(u => u.Status).HasConversion<int>().HasDefaultValue(UserStatus.PendingVerification);
        builder.Property(u => u.RowVersion).IsRowVersion();

        builder.HasOne(u => u.Association)
            .WithMany()
            .HasForeignKey(u => u.AssociationId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.RefreshTokens)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.LoginHistories)
            .WithOne(lh => lh.User)
            .HasForeignKey(lh => lh.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(u => u.CreatedAt);
        builder.HasIndex(u => u.Status);

        builder.ToTable("Users");
    }
}

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
        builder.HasIndex(r => r.Name).IsUnique();

        builder.Property(r => r.Description).HasMaxLength(500);

        builder.HasMany(r => r.UserRoles)
            .WithOne(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.RolePermissions)
            .WithOne(rp => rp.Role)
            .HasForeignKey(rp => rp.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Roles");
    }
}

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Resource).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Action).IsRequired().HasMaxLength(100);

        builder.HasIndex(p => new { p.Resource, p.Action }).IsUnique();

        builder.ToTable("Permissions");
    }
}

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(ur => ur.Id);
        builder.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
        builder.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);
        builder.HasIndex(ur => new { ur.UserId, ur.RoleId }).IsUnique();
        builder.ToTable("UserRoles");
    }
}

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(rt => rt.Id);
        builder.Property(rt => rt.Token).IsRequired().HasMaxLength(500);
        builder.Property(rt => rt.JwtId).IsRequired().HasMaxLength(500);
        builder.HasIndex(rt => rt.Token).IsUnique();
        builder.HasOne(rt => rt.User).WithMany(u => u.RefreshTokens).HasForeignKey(rt => rt.UserId);
        builder.ToTable("RefreshTokens");
    }
}

public class LoginHistoryConfiguration : IEntityTypeConfiguration<LoginHistory>
{
    public void Configure(EntityTypeBuilder<LoginHistory> builder)
    {
        builder.HasKey(lh => lh.Id);
        builder.Property(lh => lh.IpAddress).IsRequired().HasMaxLength(45);
        builder.Property(lh => lh.UserAgent).IsRequired().HasMaxLength(500);
        builder.HasOne(lh => lh.User).WithMany(u => u.LoginHistories).HasForeignKey(lh => lh.UserId);
        builder.HasIndex(lh => new { lh.UserId, lh.LoginAt });
        builder.ToTable("LoginHistories");
    }
}
