using BeautySalon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.Extensions.Paginations;

static class PaginateExtensions
{
    internal static async Task<IPageResult<T>> Page<T>(
        this IQueryable<T> query,
        IPagination pagination)
        where T : class
    {
        var pageResult = await query
            .Skip(pagination.Offset!.Value * pagination.Limit!.Value)
            .Take(pagination.Limit.Value)
            .ToListAsyncSafe();

        return new PageResult<T>(pageResult, query.Count());
    }

    internal static async Task<IPageResult<T>> Page<T>(
        this IQueryable<T> query)
        where T : class
    {
        var pageResult = await query.ToListAsyncSafe();

        return new PageResult<T>(pageResult, pageResult.Count);
    }

    private static Task<List<TSource>> ToListAsyncSafe<TSource>(
        this IQueryable<TSource> query)
    {
        if (query is null)
            throw new ArgumentNullException(nameof(query));

        if (query is not IAsyncEnumerable<TSource>)
            return Task.FromResult(query.ToList());

        return query.ToListAsync();
    }
}
