namespace InGreedIoApi.DTO
{
    public record PaginatedResponseDTO<T>(IEnumerable<T> Content, int Limit, int PageCount);
}
