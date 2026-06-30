# DAMA Grant & Qualification Management System - Backend API

Enterprise-grade REST API for grant and qualification management built with ASP.NET Core 9, Entity Framework Core, and SQL Server.

## Solution Structure

```
Backend/
├── src/
│   ├── DamaGrant.API              # REST API Layer
│   ├── DamaGrant.Application      # Business Logic Layer
│   ├── DamaGrant.Domain           # Domain Model Layer
│   ├── DamaGrant.Infrastructure   # Data Access & External Services
│   └── DamaGrant.Shared           # Shared Utilities & Constants
├── tests/
│   ├── DamaGrant.UnitTests        # Unit Tests
│   └── DamaGrant.IntegrationTests # Integration Tests
└── DamaGrant.sln                  # Solution File
```

## Technology Stack

- **Framework:** ASP.NET Core 9
- **Database:** SQL Server with Entity Framework Core 9
- **Authentication:** JWT (JSON Web Tokens)
- **Logging:** Serilog
- **Validation:** FluentValidation
- **Mapping:** AutoMapper
- **API Documentation:** Swagger/OpenAPI
- **Testing:** xUnit, Moq, FluentAssertions
- **Code Analysis:** Built-in analyzers

## Architecture

- **Clean Architecture:** Separation of concerns across layers
- **Repository Pattern:** Abstraction for data access
- **Unit of Work Pattern:** Transaction management
- **Dependency Injection:** Built-in .NET DI container
- **Role-Based Access Control (RBAC):** Fine-grained authorization
- **Global Exception Handling:** Centralized error management
- **Audit Logging:** Complete operation tracking

## Getting Started

### Prerequisites

- .NET 9 SDK
- SQL Server 2019 or higher
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
```bash
git clone <repository-url>
cd Backend
```

2. **Restore NuGet packages**
```bash
dotnet restore
```

3. **Update Database Connection String**
Edit `src/DamaGrant.API/appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=DamaGrantDb;Trusted_Connection=true;Encrypt=false;"
}
```

4. **Run Database Migrations**
```bash
dotnet ef database update --project src/DamaGrant.Infrastructure --startup-project src/DamaGrant.API
```

5. **Build the solution**
```bash
dotnet build
```

6. **Run the API**
```bash
cd src/DamaGrant.API
dotnet run
```

The API will be available at `https://localhost:7001` (or the configured port).

## API Documentation

Swagger/OpenAPI documentation is available at:
- **Development:** `https://localhost:7001`
- **Production:** `https://api.dama.sa`

## Configuration

### JWT Settings
Configure JWT in `appsettings.json`:
```json
"JwtSettings": {
  "SecretKey": "your-super-secret-key-min-32-chars",
  "Issuer": "DamaGrant",
  "Audience": "DamaGrantUsers",
  "ExpirationMinutes": 60,
  "RefreshTokenExpirationDays": 7
}
```

### Email Settings
Configure SMTP in `appsettings.json`:
```json
"EmailSettings": {
  "SmtpServer": "smtp.gmail.com",
  "SmtpPort": 587,
  "SenderEmail": "noreply@dama.sa",
  "SenderPassword": "your-app-password",
  "EnableSSL": true
}
```

### File Upload Settings
```json
"FileUpload": {
  "MaxFileSize": 52428800,
  "AllowedExtensions": ".pdf,.doc,.docx,.xls,.xlsx,.jpg,.jpeg,.png",
  "StoragePath": "/uploads"
}
```

## API Endpoints

### Authentication
- `POST /api/v1/auth/register` - Register new user
- `POST /api/v1/auth/login` - Login
- `POST /api/v1/auth/refresh` - Refresh token
- `POST /api/v1/auth/logout` - Logout

### Associations
- `GET /api/v1/associations/profile` - Get profile
- `PUT /api/v1/associations/profile` - Update profile

### Qualifications
- `POST /api/v1/qualification/applications` - Create application
- `GET /api/v1/qualification/applications/{id}` - Get application

### Grants
- `GET /api/v1/grants` - List grants
- `GET /api/v1/grants/{id}` - Get grant details

### Projects
- `GET /api/v1/projects/my-projects` - List my projects
- `POST /api/v1/projects` - Create project

See Swagger UI for complete API documentation.

## Testing

### Run Unit Tests
```bash
dotnet test tests/DamaGrant.UnitTests
```

### Run Integration Tests
```bash
dotnet test tests/DamaGrant.IntegrationTests
```

### Run All Tests
```bash
dotnet test
```

## Project Roles

1. **Association** - Grant applicant organizations
2. **Qualification Officer** - Reviews qualification applications
3. **Project Reviewer** - Reviews project proposals (technical)
4. **Financial Reviewer** - Reviews project budgets
5. **Committee Member** - Makes final decisions
6. **Grant Manager** - Manages grants and contracts
7. **Administrator** - System administration
8. **Executive User** - Executive dashboard access

## Database Schema

The database includes tables for:
- Users & Roles
- Associations & Profiles
- Qualification Applications
- Grant Opportunities
- Project Applications
- Reviews & Decisions
- Contracts & Payments
- Progress Reports
- Notifications
- Audit Logs

## Logging

Logs are written to:
- Console (Development)
- File: `Logs/log-YYYY-MM-DD.txt` (Rolling daily)

Configure log level in `appsettings.json`:
```json
"Logging": {
  "LogLevel": {
    "Default": "Information",
    "Microsoft": "Warning"
  }
}
```

## Error Handling

All errors are caught by global exception handling middleware and returned as:
```json
{
  "status": 500,
  "message": "An error occurred while processing your request",
  "detail": "Error details",
  "timestamp": "2026-06-30T10:30:00Z"
}
```

## Security

- JWT authentication for all protected endpoints
- Role-based authorization policies
- Password hashing with BCrypt
- CORS configured for specific origins
- SQL injection prevention via Entity Framework
- Request validation with FluentValidation
- Audit logging for all operations

## Health Checks

Health check endpoint: `GET /health`

Returns status of:
- SQL Server database connection
- API availability

## CI/CD Pipeline

GitHub Actions workflows configured for:
- Build & Test
- Code Quality Analysis
- Automated Deployment

## Deployment

### Docker
```bash
docker build -t dama-grant-api -f Dockerfile.backend .
docker run -p 7001:80 dama-grant-api
```

### Azure App Service
```bash
dotnet publish -c Release
# Deploy the publish folder to Azure
```

## Contributing

1. Create a feature branch
2. Make your changes
3. Write/update tests
4. Submit pull request

## License

All rights reserved © DAMA Holding Company 2026

## Support

For issues and questions:
- Email: support@dama.sa
- GitHub Issues: [Project Issues]

---

**Ready to proceed to database entity models?** (STEP 2)
