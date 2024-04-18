using InGreedIoApi.POCO;

namespace InGreedIoApi.Data.Seed
{
    public class ProductSeeder : ISeeder<ProductPOCO>
    {
        public IEnumerable<ProductPOCO> Seed =>
            [
                new ProductPOCO
                {
                    Id = 1,
                    Name = "Cow Milk",
                    IconUrl = "",
                    Description = "Low-fat milk straight from the cow.",
                    CategoryId = 1,
                },
                new ProductPOCO
                {
                    Id = 2,
                    Name = "Almond Milk",
                    IconUrl = "",
                    Description = "Dairy-free, unsweetened almond milk made from real almonds.",
                    CategoryId = 1,
                },
                new ProductPOCO
                {
                    Id = 3,
                    Name = "Oat Milk",
                    IconUrl = "",
                    Description = "Dairy-free, unsweetened oat milk made from oat.",
                    CategoryId = 1,
                }
            ];
    }
}