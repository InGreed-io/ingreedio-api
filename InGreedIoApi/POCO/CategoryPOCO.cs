namespace InGreedIoApi.POCO
{
    public class CategoryPOCO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductPOCO> Products { get; set; }
    }
}