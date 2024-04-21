using InGreedIoApi.DTO;

namespace InGreedIoApi.Services;

public class PaginationService : IPaginationService
{
    public async Task<PaginatedResponseDTO<T>> Paginate<T>(IEnumerable<T> list, int limit, int page)
    {
        if(limit == 0)
        {
          return new PaginatedResponseDTO<T>(
              new List<T>(),
              0,
              0
              );
        }

        var pageCount = (int)Math.Ceiling((double)list.Count() / (double)limit);

        return new PaginatedResponseDTO<T>(
          list.Skip(page * limit).Take(limit),
          limit,
          pageCount
          );
    }
}
