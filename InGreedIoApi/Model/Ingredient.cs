namespace InGreedIoApi.Model
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IconUrl { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}