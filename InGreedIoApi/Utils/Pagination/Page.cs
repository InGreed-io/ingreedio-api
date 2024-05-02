using System.Collections;
using AutoMapper;

namespace InGreedIoApi.Utils.Pagination
{
    public class Page<T>(IEnumerable<T> contents, PageMetadata metadata) : IPage<T>
    {
        public PageMetadata Metadata => metadata;

        public IEnumerable<T> Contents => contents;

        IEnumerable IPage.Contents => contents;
    }

    public static class Page 
    {
        public static IPage<TDestination> Convert<TSource, TDestination>(IPage<TSource> source, IMapper mapper)
        {
            return new Page<TDestination>
            (
                mapper.Map<IEnumerable<TDestination>>(source.Contents), 
                source.Metadata
            );
        }

    }
}
