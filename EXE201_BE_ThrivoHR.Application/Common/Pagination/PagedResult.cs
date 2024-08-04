namespace EXE201_BE_ThrivoHR.Application.Common.Pagination;

public class PagedResult<T>
{
    public const int DefaultPageSize = 10;
    public const int MaxPageSize = 100;
    public const int DefaultPageNumber = 1;
    public PagedResult()
    {
        Data = null!;
    }

    public static PagedResult<T> Create(
        int totalCount,
        int pageCount,
        int pageSize,
        int pageNumber,
        IEnumerable<T> data)
    {
        return new PagedResult<T>
        {
            TotalCount = totalCount,
            PageCount = pageCount,
            PageSize = pageSize <= 0 ? DefaultPageSize : pageSize > MaxPageSize ? MaxPageSize : pageSize,
            PageNumber = pageNumber <= 0 ? DefaultPageNumber : pageNumber,
            Data = data,
        };
    }

    public int TotalCount { get; set; }

    public int PageCount { get; set; }

    public int PageSize { get; set; }

    public int PageNumber { get; set; }

    public IEnumerable<T> Data { get; set; }

}
