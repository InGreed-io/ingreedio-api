using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using InGreedIoApi.Utils.Pagination;

namespace InGreedIoApi.Data.Repository.Interface;

public interface IProductRepository
{
    public Task<IPage<ReviewDTO>> GetReviews(int productId, int pageIndex, int pageSize);

    public Task<Review> AddReview(int productId, string userId, string content, float rating);

    public Task<IPage<ProductDTO>> GetAll(ProductQueryDTO productQueryDto, string? producerId = null);

    public Task<Product> GetProduct(int productId);

    public Task<Product> Create(CreateProductDTO createProductDto, string producerId);

    public Task<Product> Update(UpdateProductDTO updateProductDTO, int productId, string? producerId = null);

    public Task Delete(int productId, string? producerId = null);
}
