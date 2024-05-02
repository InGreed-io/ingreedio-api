using AutoMapper.QueryableExtensions;
using InGreedIoApi.Model.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace InGreedIoApi.Utils.Pagination
{
    public static class PaginationExtensions
    {
        public static Page<T> ToPage<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            if (pageIndex < 0) throw new InGreedIoException("PageIndex cannot be negative.");
            if (pageSize <= 0) throw new InGreedIoException("PageSize must be positive.");

            var contents = query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();

            var pageCount = (query.Count() + pageSize - 1) / pageSize; 

            return new Page<T>(contents, new PageMetadata(pageIndex, pageSize, pageCount));
        }

        public static async Task<Page<T>> ToPageAsync<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            if (pageIndex < 0) throw new InGreedIoException("PageIndex cannot be negative.");
            if (pageSize <= 0) throw new InGreedIoException("PageSize must be positive.");

            var contents = await query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var pageCount = (await query.CountAsync() + pageSize - 1) / pageSize; 

            return new Page<T>(contents, new PageMetadata(pageIndex, pageSize, pageCount));
        }

        public static Page<TDestination> ProjectToPage<TSource, TDestination>(this IQueryable<TSource> query, int pageIndex, int pageSize, AutoMapper.IConfigurationProvider configuration)
        {
            if (pageIndex < 0) throw new InGreedIoException("PageIndex cannot be negative.");
            if (pageSize <= 0) throw new InGreedIoException("PageSize must be positive.");

            var contents = query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ProjectTo<TDestination>(configuration)
                .ToList();

            var pageCount = (query.Count() + pageSize - 1) / pageSize; 

            return new Page<TDestination>(contents, new PageMetadata(pageIndex, pageSize, pageCount));
        }

        public static async Task<Page<TDestination>> ProjectToPageAsync<TSource, TDestination>(this IQueryable<TSource> query, int pageIndex, int pageSize, AutoMapper.IConfigurationProvider configuration)
        {
            if (pageIndex < 0) throw new InGreedIoException("PageIndex cannot be negative.");
            if (pageSize <= 0) throw new InGreedIoException("PageSize must be positive.");

            var contents = await query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ProjectTo<TDestination>(configuration)
                .ToListAsync();

            var pageCount = (await query.CountAsync() + pageSize - 1) / pageSize; 

            return new Page<TDestination>(contents, new PageMetadata(pageIndex, pageSize, pageCount));
        }
    }
}
