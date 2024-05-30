namespace InGreedIoApi.DTO
{
    public record GetIngredientsQuery(string? Query, int pageIndex = 0, int pageSize = 10);
}
