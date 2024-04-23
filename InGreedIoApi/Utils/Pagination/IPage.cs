using System.Collections;

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
        new PageMetadata Metadata { get; }
    }
}
