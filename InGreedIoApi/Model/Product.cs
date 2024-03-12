namespace InGreedIoApi.Model
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string IconUrl { get; set; }
        public required string Description { get; set; }
        public int? FeaturingId { get; set; }
        public required int CategoryId { get; set; }
        public required Category Category { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public Featuring? Featuring { get; set; }
    }
}