namespace InGreedIoApi.Model
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IconUrl { get; set; }

        public string Description { get; set; }

        public int? FeaturingId { get; set; }

        public int CategoryId { get; set; }

        public string ProducerId { get; set; }
        public ICollection<ApiUser> FavouriteBy { get; set; }
        public ApiUser Producer { get; set; }
        public Category Category { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public Featuring? Featuring { get; set; }
    }
}