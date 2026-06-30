namespace DamaGrant.Shared.Constants;

public static class AppConstants
{
    public const string AppName = "DAMA Grant & Qualification Management System";
    public const string AppVersion = "1.0.0";

    public static class ValidationMessages
    {
        public const string RequiredField = "This field is required.";
        public const string InvalidEmail = "Invalid email address.";
        public const string InvalidPhone = "Invalid phone number.";
        public const string PasswordTooShort = "Password must be at least 8 characters long.";
        public const string PasswordNoUpperCase = "Password must contain at least one uppercase letter.";
        public const string PasswordNoNumber = "Password must contain at least one number.";
        public const string PasswordNoSpecialChar = "Password must contain at least one special character.";
        public const string InvalidIBAN = "Invalid IBAN format.";
        public const string InvalidAmount = "Amount must be greater than zero.";
        public const string FileToLarge = "File size exceeds maximum allowed size.";
        public const string InvalidFileType = "File type is not allowed.";
    }

    public static class ErrorMessages
    {
        public const string RecordNotFound = "Record not found.";
        public const string UnauthorizedAccess = "You do not have permission to access this resource.";
        public const string InvalidCredentials = "Invalid email or password.";
        public const string EmailAlreadyExists = "This email address is already registered.";
        public const string ApplicationNotFound = "Application not found.";
        public const string InvalidStatus = "Invalid application status.";
        public const string CannotDeleteRecord = "Cannot delete this record.";
        public const string DuplicateRecord = "A record with this identifier already exists.";
    }

    public static class SuccessMessages
    {
        public const string RecordCreated = "Record created successfully.";
        public const string RecordUpdated = "Record updated successfully.";
        public const string RecordDeleted = "Record deleted successfully.";
        public const string LoginSuccessful = "Login successful.";
        public const string LogoutSuccessful = "Logout successful.";
        public const string PasswordChanged = "Password changed successfully.";
        public const string ApplicationSubmitted = "Application submitted successfully.";
        public const string ApplicationApproved = "Application approved successfully.";
    }

    public static class DefaultValues
    {
        public const int PageSize = 10;
        public const int MaxPageSize = 100;
        public const int PasswordHashIterations = 10000;
        public const int MaxLoginAttempts = 5;
        public const int LockoutDurationMinutes = 15;
        public const int TokenExpirationMinutes = 60;
        public const int RefreshTokenExpirationDays = 7;
        public const int PasswordResetTokenExpirationHours = 24;
    }

    public static class TimeFormats
    {
        public const string DateFormat = "yyyy-MM-dd";
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public const string TimeFormat = "HH:mm:ss";
    }

    public static class Currencies
    {
        public const string SAR = "SAR";
        public const string USD = "USD";
        public const string EUR = "EUR";
    }

    public static class FileTypes
    {
        public const string PDF = "application/pdf";
        public const string Word = "application/msword";
        public const string WordX = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        public const string Excel = "application/vnd.ms-excel";
        public const string ExcelX = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public const string JPG = "image/jpeg";
        public const string PNG = "image/png";
        public const string CSV = "text/csv";
    }

    public static class Roles
    {
        public const string Association = "Association";
        public const string QualificationOfficer = "QualificationOfficer";
        public const string ProjectReviewer = "ProjectReviewer";
        public const string FinancialReviewer = "FinancialReviewer";
        public const string CommitteeMember = "CommitteeMember";
        public const string GrantManager = "GrantManager";
        public const string Administrator = "Administrator";
        public const string ExecutiveUser = "ExecutiveUser";
    }

    public static class EmailTemplates
    {
        public const string WelcomeEmail = "WelcomeEmail";
        public const string ApplicationSubmitted = "ApplicationSubmitted";
        public const string ApplicationApproved = "ApplicationApproved";
        public const string ApplicationRejected = "ApplicationRejected";
        public const string ContractReady = "ContractReady";
        public const string PaymentProcessed = "PaymentProcessed";
    }

    public static class CacheKeys
    {
        public const string Users = "users";
        public const string Roles = "roles";
        public const string Permissions = "permissions";
        public const string Grants = "grants";
        public const string Settings = "settings";
        public const string UserPrefix = "user_";
        public const string GrantPrefix = "grant_";
    }
}
