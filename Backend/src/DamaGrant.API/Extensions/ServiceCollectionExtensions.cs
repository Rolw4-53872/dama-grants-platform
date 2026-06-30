using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace DamaGrant.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.Load("DamaGrant.Application"));
        return services;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.Load("DamaGrant.Application"));
        return services;
    }
}
