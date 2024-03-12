using InGreedIoApi.Model;

namespace InGreedIoApi.POCO
{
    public class CategoryPOCO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductPOCO> Products { get; set; }
    }
}