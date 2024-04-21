namespace InGreedIoApi.DTO
{
    public record PaginatedResponseDTO<T>(IEnumerable<T> content, int limit, int pageCount);
}
