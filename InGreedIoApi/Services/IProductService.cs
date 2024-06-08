using InGreedIoApi.POCO;
using InGreedIoApi.DTO;

namespace InGreedIoApi.Services;

public interface IProductService
{
    public void SortProductQueryDto(ProductQueryDTO productQueryDto, ref IQueryable<ProductPOCO> queryable, IEnumerable<int> wantedProducts);
}
