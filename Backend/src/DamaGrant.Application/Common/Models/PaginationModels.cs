namespace DamaGrant.Application.Common.Models;

public class PaginationRequest
{
    private int _pageNumber = 1;
    private int _pageSize = 10;
    private const int MaxPageSize = 100;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < 1 ? 10 : value > MaxPageSize ? MaxPageSize : value;
    }

    public string? SearchTerm { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; }
}

public class PaginatedResult<T>
{
    public List<T> Data { get; set; } = [];
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public static PaginatedResult<T> Create(List<T> data, int totalCount, int pageNumber, int pageSize)
    {
        return new PaginatedResult<T>
        {
            Data = data,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}

public class FilterRequest
{
    public string? SearchTerm { get; set; }
    public Dictionary<string, object>? Filters { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; }
}

public class SortRequest
{
    public string? SortBy { get; set; }
    public SortDirection Direction { get; set; } = SortDirection.Ascending;
}

public enum SortDirection
{
    Ascending = 0,
    Descending = 1
}

public class FilterCriteria
{
    public string FieldName { get; set; } = null!;
    public FilterOperator Operator { get; set; }
    public object? Value { get; set; }
}

public enum FilterOperator
{
    Equals = 0,
    NotEquals = 1,
    GreaterThan = 2,
    GreaterThanOrEqual = 3,
    LessThan = 4,
    LessThanOrEqual = 5,
    Contains = 6,
    StartsWith = 7,
    EndsWith = 8,
    In = 9,
    Between = 10
}
