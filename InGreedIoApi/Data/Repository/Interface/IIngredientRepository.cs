using InGreedIoApi.DTO;
using InGreedIoApi.Utils.Pagination;

namespace InGreedIoApi.Data.Repository.Interface
{
    public interface IIngredientRepository
    {
        Task<IPage<IngredientDTO>> FindAll(GetIngredientsQuery getIngredientsQuery);
    }
}
