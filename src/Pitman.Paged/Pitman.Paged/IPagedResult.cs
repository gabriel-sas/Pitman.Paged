namespace Pitman.Paged
{
    public interface IPagedResult<T>
    {
        List<T> Results { get; }

        int CurrentPage { get; }

        int PageSize { get; }

        int PageCount { get; }

        bool HasNextPage { get; }

        bool HasPreviousPage { get; }

        int TotalItemCount { get; }
    }
}