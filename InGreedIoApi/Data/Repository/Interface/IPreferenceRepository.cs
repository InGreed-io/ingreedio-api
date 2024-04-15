using InGreedIoApi.DTO;
using InGreedIoApi.POCO;

namespace InGreedIoApi.Data.Repository.Interface
{
    public interface IPreferenceRepository
    {
        Task<bool> DeleteIngredient(int preferenceId, int ingredientId);

        Task<bool> AddIngredient(int preferenceId, AddIngredientDTO addIngredientDto);

        Task<bool> Delete(int preferenceId);

        IngredientPOCO? FindById(PreferencePOCO preferencePOCO, int ingredientId);
    }
}