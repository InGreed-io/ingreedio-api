using System.ComponentModel.DataAnnotations;

namespace InGreedIoApi.DTO;

public enum ProductQuerySortBy
{
    Featured,
    Rating,
    RatingCount,
    BestMatch
}
public record ProductQueryDTO(string query, int? categoryId, ICollection<int> ingredients, int? preferenceId,
    ProductQuerySortBy? SortBy, [Range(0, int.MaxValue)] int page, int limit);