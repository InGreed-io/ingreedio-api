namespace InGreedIoApi.DTO;

public record ProductDTO(int Id, string Name, string IconUrl, float Rating,
    int RatingsCount, bool Featured, bool? Favourite = null);
