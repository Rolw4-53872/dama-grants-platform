namespace DamaGrant.Application.Common.Exceptions;

public class ApplicationException : Exception
{
    public string? Code { get; set; }
    public List<string>? Errors { get; set; }
    public int StatusCode { get; set; }

    public ApplicationException(string message, string? code = null, int statusCode = 400)
        : base(message)
    {
        Code = code;
        StatusCode = statusCode;
        Errors = [message];
    }

    public ApplicationException(string message, List<string> errors, int statusCode = 400)
        : base(message)
    {
        Errors = errors;
        StatusCode = statusCode;
    }
}

public class NotFoundException : ApplicationException
{
    public NotFoundException(string message, string? code = null)
        : base(message, code ?? "NOT_FOUND", 404)
    {
    }
}

public class ValidationException : ApplicationException
{
    public ValidationException(string message, List<string>? errors = null)
        : base(message, "VALIDATION_ERROR", 400)
    {
        Errors = errors ?? [message];
    }

    public ValidationException(Dictionary<string, List<string>> failures)
        : base("Validation failed", "VALIDATION_ERROR", 400)
    {
        Errors = failures.SelectMany(x => x.Value).ToList();
    }
}

public class UnauthorizedException : ApplicationException
{
    public UnauthorizedException(string message = "Unauthorized")
        : base(message, "UNAUTHORIZED", 401)
    {
    }
}

public class ForbiddenException : ApplicationException
{
    public ForbiddenException(string message = "Forbidden")
        : base(message, "FORBIDDEN", 403)
    {
    }
}

public class BusinessRuleException : ApplicationException
{
    public BusinessRuleException(string message, string? code = null)
        : base(message, code ?? "BUSINESS_RULE_VIOLATION", 422)
    {
    }
}

public class DuplicateException : ApplicationException
{
    public DuplicateException(string message, string? code = null)
        : base(message, code ?? "DUPLICATE_RECORD", 409)
    {
    }
}

public class InvalidOperationException : ApplicationException
{
    public InvalidOperationException(string message, string? code = null)
        : base(message, code ?? "INVALID_OPERATION", 400)
    {
    }
}
