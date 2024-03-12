using InGreedIoApi.Model;

namespace InGreedIoApi.POCO
{
    public class ProductPOCO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public string Description { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool PaymentConfirmed { get; set; }
        public int CategoryId { get; set; }
        public virtual CategoryPOCO Category { get; set; }
        public virtual ICollection<IngredientPOCO> Ingredients { get; set; }
        public virtual ICollection<ReviewPOCO> Reviews { get; set; }
    }
}