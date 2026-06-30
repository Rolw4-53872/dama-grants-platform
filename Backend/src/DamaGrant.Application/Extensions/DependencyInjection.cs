using DamaGrant.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DamaGrant.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAssociationService, AssociationService>();
        services.AddScoped<IQualificationService, QualificationService>();
        services.AddScoped<IGrantService, GrantService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<ICommitteeService, CommitteeService>();
        services.AddScoped<IContractService, ContractService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IDashboardService, DashboardService>();

        return services;
    }
}
