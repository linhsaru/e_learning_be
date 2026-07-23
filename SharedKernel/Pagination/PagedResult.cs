namespace SharedKernel.Pagination;

public sealed class PagedResult<T>
{
    public IReadOnlyCollection<T> Items { get; init; }
    public int TotalCount { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }

    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling(TotalCount / (double)PageSize) : 0;
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;

    public PagedResult(IReadOnlyCollection<T> items, int totalCount, int pageNumber, int pageSize)
    {
        Items = items ?? Array.Empty<T>();
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public PagedResult<TDestination> Map<TDestination>(Func<T, TDestination> converter)
    {
        ArgumentNullException.ThrowIfNull(converter);
        var mappedItems = Items.Select(converter).ToList();
        return new PagedResult<TDestination>(mappedItems, TotalCount, PageNumber, PageSize);
    }
}
