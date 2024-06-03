namespace InGreedIoApi.DTO;


public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string IconUrl { get; set; }
    public float Rating { get; set; }
    public int RatingsCount { get; set; }
    public bool Featured { get; set; }
    public bool? Favourite { get; set; } = null;

}
