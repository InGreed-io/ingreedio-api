using System.Collections;
using AutoMapper;

namespace InGreedIoApi.Utils.Pagination
{
    public interface IPage
    {
        IEnumerable Elements { get; }
        PageMetadata Metadata { get; }
    }

    public interface IPage<T> : IPage
    {
        new IEnumerable<T> Elements { get; }
        IPage<TDestination> MapElementsTo<TDestination>(IMapper mapper);
    }
}
