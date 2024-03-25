using InGreedIoApi.Model;

namespace InGreedIoApi.Data.Repository
{
    public interface IIngredientRepository
    {
        public Task<IEnumerable<Ingredient>> FindAll(string query, int page, int limit);
    }
}
