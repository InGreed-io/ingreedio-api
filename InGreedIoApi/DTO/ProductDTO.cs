using System.ComponentModel.DataAnnotations;

namespace InGreedIoApi.DTO;

public record ProductDTO(uint Id, string Name, string IconUrl, float Rating,
    int RatingsCount, bool Featured, bool? Favourite = null);
