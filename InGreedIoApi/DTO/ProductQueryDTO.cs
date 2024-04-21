using System.ComponentModel.DataAnnotations;
using InGreedIoApi.Model.Enum;

namespace InGreedIoApi.DTO;

public record ProductQueryDTO(string query, int? categoryId, ICollection<int>? ingredients, int? preferenceId,
    QuerySortType? SortBy, [Range(0, int.MaxValue)] int page, int limit);
