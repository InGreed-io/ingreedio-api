using InGreedIoApi.Model;

namespace InGreedIoApi.Repository
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAll();
    }
}
