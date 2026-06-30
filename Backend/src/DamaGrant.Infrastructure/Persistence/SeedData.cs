using DamaGrant.Domain.Entities;
using DamaGrant.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

namespace DamaGrant.Infrastructure.Persistence;

public static class SeedData
{
    public static async Task InitializeAsync(AppDbContext context)
    {
        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Roles.AnyAsync())
            {
                await SeedRoles(context);
            }

            if (!await context.Permissions.AnyAsync())
            {
                await SeedPermissions(context);
            }

            if (!await context.RolePermissions.AnyAsync())
            {
                await SeedRolePermissions(context);
            }

            if (!await context.Users.AnyAsync())
            {
                await SeedUsers(context);
            }

            if (!await context.Settings.AnyAsync())
            {
                await SeedSettings(context);
            }

            if (!await context.Departments.AnyAsync())
            {
                await SeedDepartments(context);
            }

            if (!await context.Lookups.AnyAsync())
            {
                await SeedLookups(context);
            }

            if (!await context.GrantPrograms.AnyAsync())
            {
                await SeedGrantPrograms(context);
            }

            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error seeding database: {ex.Message}");
            throw;
        }
    }

    private static async Task SeedRoles(AppDbContext context)
    {
        var roles = new List<Role>
        {
            new() { Id = 1, Name = "Association", Description = "Nonprofit organization" },
            new() { Id = 2, Name = "QualificationOfficer", Description = "Qualification reviewer" },
            new() { Id = 3, Name = "ProjectReviewer", Description = "Project reviewer" },
            new() { Id = 4, Name = "FinancialReviewer", Description = "Financial reviewer" },
            new() { Id = 5, Name = "CommitteeMember", Description = "Committee member" },
            new() { Id = 6, Name = "GrantManager", Description = "Grant manager" },
            new() { Id = 7, Name = "Administrator", Description = "System administrator" },
            new() { Id = 8, Name = "ExecutiveUser", Description = "Executive user" }
        };

        await context.Roles.AddRangeAsync(roles);
    }

    private static async Task SeedPermissions(AppDbContext context)
    {
        var permissions = new List<Permission>
        {
            new() { Id = 1, Name = "ViewDashboard", Resource = "Dashboard", Action = "View" },
            new() { Id = 2, Name = "SubmitApplication", Resource = "Application", Action = "Submit" },
            new() { Id = 3, Name = "ReviewApplication", Resource = "Application", Action = "Review" },
            new() { Id = 4, Name = "ApproveApplication", Resource = "Application", Action = "Approve" },
            new() { Id = 5, Name = "ManageUsers", Resource = "User", Action = "Manage" },
            new() { Id = 6, Name = "ViewReports", Resource = "Report", Action = "View" },
            new() { Id = 7, Name = "ManageGrants", Resource = "Grant", Action = "Manage" },
            new() { Id = 8, Name = "ProcessPayments", Resource = "Payment", Action = "Process" },
            new() { Id = 9, Name = "ViewAuditLog", Resource = "AuditLog", Action = "View" },
            new() { Id = 10, Name = "ManageSettings", Resource = "Settings", Action = "Manage" }
        };

        await context.Permissions.AddRangeAsync(permissions);
    }

    private static async Task SeedRolePermissions(AppDbContext context)
    {
        var rolePermissions = new List<RolePermission>
        {
            new() { Id = 1, RoleId = 1, PermissionId = 1 },
            new() { Id = 2, RoleId = 1, PermissionId = 2 },
            new() { Id = 3, RoleId = 2, PermissionId = 3 },
            new() { Id = 4, RoleId = 3, PermissionId = 3 },
            new() { Id = 5, RoleId = 4, PermissionId = 3 },
            new() { Id = 6, RoleId = 5, PermissionId = 4 },
            new() { Id = 7, RoleId = 6, PermissionId = 4 },
            new() { Id = 8, RoleId = 7, PermissionId = 5 },
            new() { Id = 9, RoleId = 7, PermissionId = 10 },
            new() { Id = 10, RoleId = 8, PermissionId = 1 }
        };

        await context.RolePermissions.AddRangeAsync(rolePermissions);
    }

    private static async Task SeedUsers(AppDbContext context)
    {
        var passwordHasher = new PasswordHashService();

        var users = new List<User>
        {
            new()
            {
                Id = 1,
                Email = "admin@dama.sa",
                PhoneNumber = "0501234567",
                FirstName = "Admin",
                LastName = "User",
                PasswordHash = passwordHasher.HashPassword("Admin@123"),
                Status = UserStatus.Active,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = 2,
                Email = "officer@dama.sa",
                PhoneNumber = "0502234567",
                FirstName = "Qualification",
                LastName = "Officer",
                PasswordHash = passwordHasher.HashPassword("Officer@123"),
                Status = UserStatus.Active,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = 3,
                Email = "reviewer@dama.sa",
                PhoneNumber = "0503234567",
                FirstName = "Project",
                LastName = "Reviewer",
                PasswordHash = passwordHasher.HashPassword("Reviewer@123"),
                Status = UserStatus.Active,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                CreatedAt = DateTime.UtcNow
            }
        };

        await context.Users.AddRangeAsync(users);

        var userRoles = new List<UserRole>
        {
            new() { Id = 1, UserId = 1, RoleId = 7, AssignedAt = DateTime.UtcNow },
            new() { Id = 2, UserId = 2, RoleId = 2, AssignedAt = DateTime.UtcNow },
            new() { Id = 3, UserId = 3, RoleId = 3, AssignedAt = DateTime.UtcNow }
        };

        await context.UserRoles.AddRangeAsync(userRoles);
    }

    private static async Task SeedSettings(AppDbContext context)
    {
        var settings = new List<Settings>
        {
            new() { Id = 1, SettingKey = "MaxApplicationsPerAssociation", SettingValue = "5", SettingType = "Integer" },
            new() { Id = 2, SettingKey = "QualificationValidityYears", SettingValue = "3", SettingType = "Integer" },
            new() { Id = 3, SettingKey = "MaxUploadFileSize", SettingValue = "52428800", SettingType = "Integer" },
            new() { Id = 4, SettingKey = "SystemEmail", SettingValue = "noreply@dama.sa", SettingType = "String" },
            new() { Id = 5, SettingKey = "SupportEmail", SettingValue = "support@dama.sa", SettingType = "String" }
        };

        await context.Settings.AddRangeAsync(settings);
    }

    private static async Task SeedDepartments(AppDbContext context)
    {
        var departments = new List<Department>
        {
            new() { Id = 1, DepartmentName = "Finance", Description = "Finance Department", IsActive = true, DisplayOrder = 1 },
            new() { Id = 2, DepartmentName = "Grants", Description = "Grants Department", IsActive = true, DisplayOrder = 2 },
            new() { Id = 3, DepartmentName = "Compliance", Description = "Compliance Department", IsActive = true, DisplayOrder = 3 },
            new() { Id = 4, DepartmentName = "IT", Description = "Information Technology", IsActive = true, DisplayOrder = 4 }
        };

        await context.Departments.AddRangeAsync(departments);
    }

    private static async Task SeedLookups(AppDbContext context)
    {
        var lookup = new Lookup { Id = 1, LookupName = "ApplicationStatus", LookupType = "Enum", IsActive = true };
        await context.Lookups.AddAsync(lookup);

        var values = new List<LookupValue>
        {
            new() { Id = 1, LookupId = 1, ValueCode = "DRAFT", ValueText = "Draft", DisplayOrder = 1, IsActive = true },
            new() { Id = 2, LookupId = 1, ValueCode = "SUBMITTED", ValueText = "Submitted", DisplayOrder = 2, IsActive = true },
            new() { Id = 3, LookupId = 1, ValueCode = "APPROVED", ValueText = "Approved", DisplayOrder = 3, IsActive = true },
            new() { Id = 4, LookupId = 1, ValueCode = "REJECTED", ValueText = "Rejected", DisplayOrder = 4, IsActive = true }
        };

        await context.LookupValues.AddRangeAsync(values);
    }

    private static async Task SeedGrantPrograms(AppDbContext context)
    {
        var program = new GrantProgram
        {
            Id = 1,
            Name = "Community Empowerment",
            Description = "Supporting local communities",
            LaunchDate = DateTime.UtcNow,
            IsActive = true,
            TotalBudget = 10000000,
            AllocatedBudget = 0,
            CreatedAt = DateTime.UtcNow
        };

        await context.GrantPrograms.AddAsync(program);
    }
}
