namespace InGreedIoApi.DTO
{
    public record GetIngredientsQuery(string? Query, int[]? Include, int[]? Exclude, int pageIndex = 0, int pageSize = 10);
}
