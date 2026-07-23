using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Pagination
{
    public static class PaginationExtensions
    {
        public static IQueryable<T> Paginate<T>(
            this IQueryable<T> query,
            PageRequest pageRequest)
        {
            return query
                .Skip(pageRequest.Skip)
                .Take(pageRequest.PageSize);
        }
    }
}
