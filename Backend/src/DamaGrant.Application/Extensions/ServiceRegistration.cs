using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DamaGrant.Application.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationLayer(
        this IServiceCollection services)
    {
        AddMediatR(services);
        AddAutoMapper(services);
        AddFluentValidation(services);
        AddApplicationServices(services);
        AddPipelineBehaviors(services);

        return services;
    }

    private static void AddMediatR(IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly);
        });
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }

    private static void AddFluentValidation(IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
    }

    private static void AddApplicationServices(IServiceCollection services)
    {
        // Authentication Services
        // services.AddScoped<IAuthenticationService, AuthenticationService>();

        // User Services
        // services.AddScoped<IUserService, UserService>();

        // Association Services
        // services.AddScoped<IAssociationService, AssociationService>();

        // Qualification Services
        // services.AddScoped<IQualificationService, QualificationService>();

        // Grant Services
        // services.AddScoped<IGrantProgramService, GrantProgramService>();
        // services.AddScoped<IGrantOpportunityService, GrantOpportunityService>();

        // Project Services
        // services.AddScoped<IProjectApplicationService, ProjectApplicationService>();

        // Review Services
        // services.AddScoped<ITechnicalReviewService, TechnicalReviewService>();
        // services.AddScoped<IFinancialReviewService, FinancialReviewService>();
        // services.AddScoped<ICommitteeReviewService, CommitteeReviewService>();

        // Contract Services
        // services.AddScoped<IContractService, ContractService>();

        // Payment Services
        // services.AddScoped<IPaymentService, PaymentService>();

        // Report Services
        // services.AddScoped<IProgressReportService, ProgressReportService>();
        // services.AddScoped<IReportService, ReportService>();

        // Notification Services
        // services.AddScoped<INotificationService, NotificationService>();

        // Dashboard Services
        // services.AddScoped<IDashboardService, DashboardService>();

        // Administration Services
        // services.AddScoped<IAdministrationService, AdministrationService>();
    }

    private static void AddPipelineBehaviors(IServiceCollection services)
    {
        // Pipeline behaviors will be registered here for cross-cutting concerns:
        // - Validation
        // - Logging
        // - Performance monitoring
        // - Authorization
        // - Caching
    }
}

/// <summary>
/// Application Layer Services Configuration
///
/// This extension method registers all application layer services including:
/// - MediatR for CQRS pattern implementation
/// - AutoMapper for object mapping
/// - FluentValidation for validation rules
/// - All domain services for business logic
/// - Pipeline behaviors for cross-cutting concerns
///
/// Usage in Startup:
/// services.AddApplicationLayer();
/// </summary>
