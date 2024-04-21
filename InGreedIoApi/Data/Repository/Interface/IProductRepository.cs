using InGreedIoApi.DTO;

namespace InGreedIoApi.Data.Repository.Interface;

public interface IProductRepository
{
    public Task<IEnumerable<ProductDTO>> GetAll(ProductQueryDTO productQueryDto);
}
