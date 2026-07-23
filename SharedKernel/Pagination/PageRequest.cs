using SharedKernel.Constants;

namespace SharedKernel.Pagination;

public sealed record PageRequest
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }

    public int Skip => (PageNumber - 1) * PageSize;

    public PageRequest(int pageNumber = CommonConstants.DefaultPage, int pageSize = CommonConstants.DefaultPageSize)
    {
        PageNumber = pageNumber < 1 ? CommonConstants.DefaultPage : pageNumber;
        PageSize = pageSize < 1 ? CommonConstants.DefaultPageSize :
                   pageSize > CommonConstants.DefaultMaxPageSize ? CommonConstants.DefaultMaxPageSize : pageSize;
    }
}
