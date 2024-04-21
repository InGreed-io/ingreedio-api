using InGreedIoApi.DTO;

namespace InGreedIoApi.Services;

public interface IPaginationService
{
    public Task<PaginatedResponseDTO<T>> Paginate<T>(IEnumerable<T> list, int limit, int page);
}
