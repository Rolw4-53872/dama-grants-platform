using DamaGrant.API.Extensions;
using DamaGrant.API.Middlewares;
using DamaGrant.Application.Extensions;
using DamaGrant.Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "DamaGrant.API")
    .CreateLogger();

try
{
    Log.Information("Starting DamaGrant API...");

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });

        options.AddPolicy("AllowSpecific", policy =>
        {
            policy.WithOrigins(builder.Configuration["Cors:AllowedOrigins"]?.Split(",") ?? Array.Empty<string>())
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "DAMA Grant & Qualification Management System API",
            Version = "v1",
            Description = "Enterprise REST API for grant and qualification management",
            TermsOfService = new Uri("https://dama.sa/terms"),
            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            {
                Name = "DAMA Support",
                Email = "support@dama.sa"
            }
        });

        var xmlFile = $"{typeof(Program).Assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }

        options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });

        options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
    });

    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    });

    builder.Services.AddVersionedApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

    builder.Services.AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
        options.Providers.Add<System.IO.Compression.GzipCompressionProvider>();
    });

    builder.Services.AddHealthChecks()
        .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? "");

    builder.Services.AddAuthentication();
    builder.Services.AddAuthentication(builder.Configuration);

    builder.Services.AddAuthorizationPolicies();

    builder.Services.AddFluentValidation();

    builder.Services.AddAutoMapper();

    builder.Services.AddApplicationServices();

    builder.Services.AddInfrastructureServices(builder.Configuration);

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "DAMA Grant API V1");
            options.RoutePrefix = string.Empty;
            options.DisplayRequestDuration();
        });
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseResponseCompression();

    app.UseCors("AllowSpecific");

    app.UseRouting();

    app.UseAuthentication();

    app.UseAuthorization();

    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

    app.UseMiddleware<AuditLoggingMiddleware>();

    app.MapControllers();

    app.MapHealthChecks("/health");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
