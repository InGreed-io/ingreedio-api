using System.Collections;

namespace InGreedIoApi.Utils.Pagination
{
    public interface IPage
    {
        IEnumerable Contents { get; }
        PageMetadata Metadata { get; }
    }

    public interface IPage<T> : IPage
    {
        new IEnumerable<T> Contents { get; }
    }
}
