using InGreedIoApi.Model;

namespace InGreedIoApi.Data.Repository.Interface
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<Ingredient>> FindAll(string query);
    }
}
