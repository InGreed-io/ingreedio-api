namespace InGreedIoApi.DTO
{
    public record CreateProductDTO(
        string Name,
        IEnumerable<int> Ingredients,
        int CategoryId,
        string Description,
        IFormFile Photo);
}
