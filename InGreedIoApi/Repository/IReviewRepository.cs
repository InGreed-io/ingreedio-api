using InGreedIoApi.Model;

namespace InGreedIoApi.Repository
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAll();

        Task<Review> Report(int reviewId);
    }
}
