using InGreedIoApi.Model;

namespace InGreedIoApi.DTO
{
    public record PreferenceDTO(int Id, string Name, ICollection<IngredientDTO> Wanted, ICollection<IngredientDTO> Unwanted);
}