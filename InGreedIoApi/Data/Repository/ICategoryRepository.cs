using InGreedIoApi.Model;

namespace InGreedIoApi.Data.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
    }
}