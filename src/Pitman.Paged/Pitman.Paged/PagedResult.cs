using Microsoft.EntityFrameworkCore;

namespace Pitman.Paged
{

    public class PagedResult<T> : IPagedResult<T>
    {
        public PagedResult(List<T> items, int count, int pageIndex, int pageSize)
        {
            CurrentPage = pageIndex;
            PageCount = (int)Math.Ceiling(count / (double)pageSize);
            TotalItemCount = count;
            PageSize = pageSize;
            Results.AddRange(items);
        }

        public int CurrentPage { get; }
        public bool HasNextPage => CurrentPage < PageCount;
        public bool HasPreviousPage => CurrentPage > 1;
        public int PageCount { get; }
        public int PageSize { get; }
        public List<T> Results { get; } = new List<T>();
        public int TotalItemCount { get; }

        public static async Task<PagedResult<T>> GetResultsAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedResult<T>(items, count, pageIndex, pageSize);
        }
    }
}