using InGreedIoApi.Model;

namespace InGreedIoApi.Data.Repository
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAll();
    }
}
