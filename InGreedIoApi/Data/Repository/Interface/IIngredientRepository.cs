using InGreedIoApi.DTO;
using InGreedIoApi.Model;
using InGreedIoApi.POCO;

namespace InGreedIoApi.Data.Repository.Interface
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<Ingredient>> FindAll(string query, int page, int limit);
    }
}