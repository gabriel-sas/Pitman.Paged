using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Pitman.Paged
{
    public static class PagedResultExtensions
    {
        public static async Task<PagedResult<T>> GetPaged<T>(this IQueryable<T> query,
                                            int page, int pageSize)
        {
            return await PagedResult<T>.GetResultsAsync(query, page, pageSize);
        }

        public static async Task<PagedResult<T>> GetPaged<T, TProperty>(this IIncludableQueryable<T, TProperty> query,
                                    int page, int pageSize)
        {
            return await PagedResult<T>.GetResultsAsync(query, page, pageSize);
        }

        public static Task<PagedResult<TProperty>> GetPaged<T, TProperty>(this PagedResult<T> pagedResult, Expression<Func<T, TProperty>> selector)
        {
            var newPage = new PagedResult<TProperty>(pagedResult.Results.Select(selector.Compile()).ToList(), pagedResult.TotalItemCount, pagedResult.CurrentPage, pagedResult.PageSize);
            return Task.FromResult(newPage);
        }
    }
}