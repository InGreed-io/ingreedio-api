using InGreedIoApi.Model;

namespace InGreedIoApi.POCO
{
    public class IngredientPOCO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}