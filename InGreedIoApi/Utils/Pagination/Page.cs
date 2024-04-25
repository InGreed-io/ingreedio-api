using System.Collections;
using AutoMapper;

namespace InGreedIoApi.Utils.Pagination
{
    public class Page<T>(IEnumerable<T> elements, int pageNumber, int pageSize, bool isLast)
        : IPage<T>
    {
        public PageMetadata Metadata => new PageMetadata(pageNumber, pageSize, isLast);

        public IEnumerable<T> Elements => elements;

        IEnumerable IPage.Elements => elements;

        public IPage<TDestination> MapElementsTo<TDestination>(IMapper mapper)
        {
            return new Page<TDestination>
            (
                mapper.Map<IEnumerable<TDestination>>(elements), 
                pageNumber, pageSize, isLast
            );
        }
    }
}
