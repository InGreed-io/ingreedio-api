using InGreedIoApi.POCO;

namespace InGreedIoApi.Data.Seed
{
    public class CategorySeeder : ISeeder<CategoryPOCO>
    {
        public IEnumerable<CategoryPOCO> Seed =>
            [
                new CategoryPOCO
                {
                    Id = 1,
                    Name = "Food",
                    Products = null
                },
                new CategoryPOCO
                {
                    Id = 2,
                    Name = "Cosmetics",
                    Products = null
                },
                new CategoryPOCO
                {
                    Id = 3,
                    Name = "Drink",
                    Products = null
                },

            ];
    }
}