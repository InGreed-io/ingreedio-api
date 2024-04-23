using System.Collections;

namespace InGreedIoApi.Utils.Pagination
{
    public class Page<T>(IEnumerable<T> Elements, int PageNumber, int PageSize, bool IsLast)
        : IPage<T>
    {
        public PageMetadata Metadata => new PageMetadata(PageNumber, PageSize, IsLast);

        IEnumerable<T> IPage<T>.Elements => Elements;

        IEnumerable IPage.Elements => Elements;
    }
}
