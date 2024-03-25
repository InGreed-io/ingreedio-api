using InGreedIoApi.Model;

namespace InGreedIoApi.Repository
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAll();

        Task<Review?> Report(int reviewId);

        Task<Review?> Rate(int reviewId, float reviewRating);

        Task<Review?> Update(int reviewId, string content, float reviewRating);
    }
}
