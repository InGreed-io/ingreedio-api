namespace InGreedIoApi.POCO
{
    public class IngredientPOCO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public ICollection<ProductPOCO> Products { get; set; }
    }
}