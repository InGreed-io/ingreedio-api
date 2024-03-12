namespace InGreedIoApi.Model
{
    public class Ingredient
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string IconUrl { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}