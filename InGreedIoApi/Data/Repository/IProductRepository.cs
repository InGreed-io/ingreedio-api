using InGreedIoApi.DTO;
using InGreedIoApi.Model;

namespace InGreedIoApi.Data.Repository;

public interface IProductRepository
{
    public Task<IEnumerable<Product>> GetAll(ProductQueryDTO productQueryDto);
}