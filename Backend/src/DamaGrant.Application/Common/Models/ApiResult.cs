namespace DamaGrant.Application.Common.Models;

public class ApiResult<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }
    public int? StatusCode { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public static ApiResult<T> SuccessResult(T data, string message = "Operation successful")
    {
        return new ApiResult<T>
        {
            Success = true,
            Data = data,
            Message = message,
            StatusCode = 200
        };
    }

    public static ApiResult<T> CreatedResult(T data, string message = "Resource created successfully")
    {
        return new ApiResult<T>
        {
            Success = true,
            Data = data,
            Message = message,
            StatusCode = 201
        };
    }

    public static ApiResult<T> FailureResult(string message, List<string>? errors = null, int statusCode = 400)
    {
        return new ApiResult<T>
        {
            Success = false,
            Message = message,
            Errors = errors,
            StatusCode = statusCode
        };
    }

    public static ApiResult<T> NotFoundResult(string message = "Resource not found")
    {
        return new ApiResult<T>
        {
            Success = false,
            Message = message,
            StatusCode = 404,
            Errors = new List<string> { message }
        };
    }

    public static ApiResult<T> UnauthorizedResult(string message = "Unauthorized")
    {
        return new ApiResult<T>
        {
            Success = false,
            Message = message,
            StatusCode = 401,
            Errors = new List<string> { message }
        };
    }

    public static ApiResult<T> ForbiddenResult(string message = "Forbidden")
    {
        return new ApiResult<T>
        {
            Success = false,
            Message = message,
            StatusCode = 403,
            Errors = new List<string> { message }
        };
    }
}

public class ApiResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
    public int? StatusCode { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public static ApiResult SuccessResult(string message = "Operation successful")
    {
        return new ApiResult
        {
            Success = true,
            Message = message,
            StatusCode = 200
        };
    }

    public static ApiResult CreatedResult(string message = "Resource created successfully")
    {
        return new ApiResult
        {
            Success = true,
            Message = message,
            StatusCode = 201
        };
    }

    public static ApiResult FailureResult(string message, List<string>? errors = null, int statusCode = 400)
    {
        return new ApiResult
        {
            Success = false,
            Message = message,
            Errors = errors,
            StatusCode = statusCode
        };
    }

    public static ApiResult NotFoundResult(string message = "Resource not found")
    {
        return new ApiResult
        {
            Success = false,
            Message = message,
            StatusCode = 404,
            Errors = new List<string> { message }
        };
    }

    public static ApiResult UnauthorizedResult(string message = "Unauthorized")
    {
        return new ApiResult
        {
            Success = false,
            Message = message,
            StatusCode = 401,
            Errors = new List<string> { message }
        };
    }

    public static ApiResult ForbiddenResult(string message = "Forbidden")
    {
        return new ApiResult
        {
            Success = false,
            Message = message,
            StatusCode = 403,
            Errors = new List<string> { message }
        };
    }
}
