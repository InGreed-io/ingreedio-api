using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace InGreedIoApi.Utils.Pagination
{
    public static class PaginationExtensions
    {
        public static Page<T> ToPage<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
        {
            var elements = queryable
                .Skip(pageNumber * pageSize)
                .Take(pageSize + 1)
                .ToList();

            return CreatePage(elements, pageNumber, pageSize);
        }

        public static async Task<Page<T>> ToPageAsync<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
        {
            var elements = await queryable
                .Skip(pageNumber * pageSize)
                .Take(pageSize + 1)
                .ToListAsync();

            return CreatePage(elements, pageNumber, pageSize);
        }

        public static Page<DstT> ProjectToPage<SrcT, DstT>(this IQueryable<SrcT> queryable, int pageNumber, int pageSize, AutoMapper.IConfigurationProvider configuration)
        {
            var elements = queryable
                .Skip(pageNumber * pageSize)
                .Take(pageSize + 1)
                .ProjectTo<DstT>(configuration)
                .ToList();

            return CreatePage(elements, pageNumber, pageSize);
        }

        public static async Task<Page<DstT>> ProjectToPageAsync<SrcT, DstT>(this IQueryable<SrcT> queryable, int pageNumber, int pageSize, AutoMapper.IConfigurationProvider configuration)
        {
            var elements = await queryable
                .Skip(pageNumber * pageSize)
                .Take(pageSize + 1)
                .ProjectTo<DstT>(configuration)
                .ToListAsync();

            return CreatePage(elements, pageNumber, pageSize);
        }

        private static Page<T> CreatePage<T>(IList<T> elements, int pageNumber, int pageSize)
        {
            if (elements.Count == pageSize + 1)
            {
                elements.RemoveAt(pageSize);
                return new Page<T>(elements, pageNumber, pageSize, false);
            }

            return new Page<T>(elements, pageNumber, pageSize, true);
        }
    }
}
