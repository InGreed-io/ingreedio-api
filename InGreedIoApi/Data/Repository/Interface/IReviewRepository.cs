using InGreedIoApi.DTO;
using InGreedIoApi.Model;

namespace InGreedIoApi.Data.Repository.Interface
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAll(string userId);

        Task<Review?> Report(int reviewId);

        Task<Review?> Rate(int reviewId, float reviewRating);

        Task<Review?> Update(int reviewId, ReviewUpdateDTO reviewUpdateDto);

        Task<IEnumerable<Review>> GetForProduct(int productId);
    }
}