using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using InGreedIoApi.Utils.Pagination;

namespace InGreedIoApi.Data.Repository.Interface;

public interface IProductRepository
{
    public Task<IPage<ReviewDTO>> GetReviews(int productId, int pageIndex, int pageSize);

    public Task<Review> AddReview(int productId, string userId, string content, float rating);

    public Task<IEnumerable<ProductDTO>> GetAll(ProductQueryDTO productQueryDto);
}
