using InGreedIoApi.Model;

namespace InGreedIoApi.POCO
{
    public class ProductPOCO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public string Description { get; set; }
        public int? FeaturingId { get; set; }
        public int CategoryId { get; set; }
        public CategoryPOCO Category { get; set; }
        public ICollection<IngredientPOCO> Ingredients { get; set; }
        public ICollection<ReviewPOCO> Reviews { get; set; }
        public FeaturingPOCO? Featuring { get; set; }
        public string ProducerId { get; set; }
        public ApiUser Producer { get; set; }
        public ICollection<ApiUser> FavouriteBy { get; set; }
    }
}