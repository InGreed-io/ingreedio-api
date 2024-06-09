using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using InGreedIoApi.Utils.Pagination;

namespace InGreedIoApi.Data.Repository.Interface
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAll(string userId);

        Task<IPage<ReportedReviewDTO>> GetReported(int pageIndex, int pageSize);

        Task<Review?> Report(int reviewId);

        Task<Review?> Rate(int reviewId, float reviewRating);

        Task<Review?> Update(int reviewId, ReviewUpdateDTO reviewUpdateDto);

        Task<IEnumerable<Review>> GetForProduct(int productId);

        Task<Review?> Create(CreateReviewDTO createReviewDto);

        Task Delete(int reviewId, string? userId = null);
    }
}
