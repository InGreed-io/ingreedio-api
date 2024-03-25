using InGreedIoApi.Model;

namespace InGreedIoApi.Data.Repository
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<Ingredient>> FindAll(string query, int page, int limit);
    }
}