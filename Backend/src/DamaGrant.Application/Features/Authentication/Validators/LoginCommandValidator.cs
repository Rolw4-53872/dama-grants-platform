using DamaGrant.Application.Common.Validators;
using DamaGrant.Application.Features.Authentication.Commands;
using FluentValidation;

namespace DamaGrant.Application.Features.Authentication.Validators;

public class LoginCommandValidator : BaseValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .ValidEmail("Email");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters");
    }
}

public class RegisterCommandValidator : BaseValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .ValidEmail("Email");

        RuleFor(x => x.PhoneNumber)
            .ValidPhone("PhoneNumber");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .Length(2, 100).WithMessage("First name must be between 2 and 100 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .Length(2, 100).WithMessage("Last name must be between 2 and 100 characters");

        RuleFor(x => x.Password)
            .ValidPassword("Password");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match");

        RuleFor(x => x.UserRole)
            .NotEmpty().WithMessage("User role is required")
            .Must(x => new[] { "Association", "Administrator" }.Contains(x))
            .WithMessage("Invalid user role");
    }
}

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty().WithMessage("Access token is required");

        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token is required");
    }
}

public class ChangePasswordCommandValidator : BaseValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User ID is invalid");

        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("Current password is required");

        RuleFor(x => x.NewPassword)
            .ValidPassword("NewPassword");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.NewPassword).WithMessage("Passwords do not match");

        RuleFor(x => x)
            .Must(x => x.CurrentPassword != x.NewPassword)
            .WithMessage("New password must be different from current password")
            .OverridePropertyName("NewPassword");
    }
}

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address");
    }
}

public class ResetPasswordCommandValidator : BaseValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .ValidEmail("Email");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Reset token is required");

        RuleFor(x => x.NewPassword)
            .ValidPassword("NewPassword");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.NewPassword).WithMessage("Passwords do not match");
    }
}

public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
{
    public VerifyEmailCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User ID is invalid");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Verification token is required");
    }
}

public class ConfirmPhoneCommandValidator : AbstractValidator<ConfirmPhoneCommand>
{
    public ConfirmPhoneCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User ID is invalid");

        RuleFor(x => x.VerificationCode)
            .NotEmpty().WithMessage("Verification code is required")
            .Length(6, 6).WithMessage("Verification code must be 6 digits");
    }
}
