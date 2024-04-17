using InGreedIoApi.DTO;
using InGreedIoApi.Model;

namespace InGreedIoApi.Data.Repository.Interface;

public interface IProductRepository
{
    public Task<IEnumerable<Product>> GetAll(ProductQueryDTO productQueryDto);
    public Task<IEnumerable<Review>> GetReviews(int productId, int page, int limit);
    public Task<Review> AddReview(int productId, string userId, string content, float rating);
}
