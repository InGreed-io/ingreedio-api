namespace InGreedIoApi.DTO
{
    public record CreateProductDTO(
        string Name,
        IEnumerable<int> Ingredients,
        int CompanyId,
        int CategoryId,
        string Description);
}