using InGreedIoApi.Model;

namespace InGreedIoApi.Data.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
    }
}