using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DamaGrant.API.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"];
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];

        if (string.IsNullOrWhiteSpace(secretKey))
            throw new InvalidOperationException("JWT SecretKey is not configured");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception is SecurityTokenExpiredException)
                    {
                        context.Response.Headers.Add("Token-Expired", "true");
                    }
                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }

    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AssociationPolicy", policy =>
                policy.RequireRole("Association", "Administrator", "ExecutiveUser"));

            options.AddPolicy("QualificationOfficerPolicy", policy =>
                policy.RequireRole("QualificationOfficer", "Administrator", "ExecutiveUser"));

            options.AddPolicy("ProjectReviewerPolicy", policy =>
                policy.RequireRole("ProjectReviewer", "Administrator", "ExecutiveUser"));

            options.AddPolicy("FinancialReviewerPolicy", policy =>
                policy.RequireRole("FinancialReviewer", "Administrator", "ExecutiveUser"));

            options.AddPolicy("CommitteeMemberPolicy", policy =>
                policy.RequireRole("CommitteeMember", "Administrator", "ExecutiveUser"));

            options.AddPolicy("GrantManagerPolicy", policy =>
                policy.RequireRole("GrantManager", "Administrator", "ExecutiveUser"));

            options.AddPolicy("AdministratorPolicy", policy =>
                policy.RequireRole("Administrator"));

            options.AddPolicy("ExecutivePolicy", policy =>
                policy.RequireRole("ExecutiveUser", "Administrator"));

            options.AddPolicy("AuthenticatedPolicy", policy =>
                policy.RequireAuthenticatedUser());
        });

        return services;
    }
}
