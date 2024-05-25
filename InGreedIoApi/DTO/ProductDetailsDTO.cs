using InGreedIoApi.Model;

namespace InGreedIoApi.DTO
{
    public class ProductDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public float Rating { get; set; }
        public int RatingsCount { get; set; }
        public bool Featured { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
        public ICollection<ProductRevewiDTO> Reviews { get; set; } = [];
        public bool? Favourite { get; set; } = null;

        public ProductDetailsDTO(int id, string name, string iconUrl, float rating, int ratingsCount, bool featured, string companyName, string description, List<string> ingredients, bool? favourite)
        {
            Id = id;
            Name = name;
            IconUrl = iconUrl;
            Rating = rating;
            RatingsCount = ratingsCount;
            Featured = featured;
            CompanyName = companyName;
            Description = description;
            Ingredients = ingredients;
            Favourite = favourite;
        }
    }
}