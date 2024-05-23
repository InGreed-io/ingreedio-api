namespace InGreedIoApi.DTO
{
    public record UpdateProductDTO(string Name, List<int> Ingredients, string Description);
}