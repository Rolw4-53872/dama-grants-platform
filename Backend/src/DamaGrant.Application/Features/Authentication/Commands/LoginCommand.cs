using DamaGrant.Application.Common.Models;
using MediatR;

namespace DamaGrant.Application.Features.Authentication.Commands;

public class LoginCommand : IRequest<LoginResponse>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LoginResponse
{
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public List<string> Roles { get; set; } = [];
    public List<string> Permissions { get; set; } = [];
}

public class RegisterCommand : IRequest<RegisterResponse>
{
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public string UserRole { get; set; } = null!;
}

public class RegisterResponse
{
    public int UserId { get; set; }
    public string Email { get; set; } = null!;
    public string Message { get; set; } = null!;
    public bool EmailVerificationRequired { get; set; }
}

public class RefreshTokenCommand : IRequest<LoginResponse>
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}

public class LogoutCommand : IRequest<ApiResult>
{
    public int UserId { get; set; }
}

public class ChangePasswordCommand : IRequest<ApiResult>
{
    public int UserId { get; set; }
    public string CurrentPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}

public class ForgotPasswordCommand : IRequest<ApiResult>
{
    public string Email { get; set; } = null!;
}

public class ResetPasswordCommand : IRequest<ApiResult>
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}

public class VerifyEmailCommand : IRequest<ApiResult>
{
    public int UserId { get; set; }
    public string Token { get; set; } = null!;
}

public class ConfirmPhoneCommand : IRequest<ApiResult>
{
    public int UserId { get; set; }
    public string VerificationCode { get; set; } = null!;
}

public class EnableTwoFactorCommand : IRequest<ApiResult>
{
    public int UserId { get; set; }
}

public class DisableTwoFactorCommand : IRequest<ApiResult>
{
    public int UserId { get; set; }
}
