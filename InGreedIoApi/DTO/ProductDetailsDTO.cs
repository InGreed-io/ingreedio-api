namespace InGreedIoApi.DTO
{
    public class ProductDetailsDTO(int Id, string Name, string IconUrl, float Rating,
    int RatingsCount, bool Featured, string CompanyName, string Description, bool? Favourite = null)
    {
        public bool Favourite { get; internal set; }
    }
}