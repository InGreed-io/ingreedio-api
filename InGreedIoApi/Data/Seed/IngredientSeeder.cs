using InGreedIoApi.POCO;

namespace InGreedIoApi.Data.Seed
{
    public class IngredientSeeder : ISeeder<IngredientPOCO>
    {
        public IEnumerable<IngredientPOCO> Seed =>
            [
                new IngredientPOCO
                {
                    Id = 1,
                    Name = "Cinamon",
                    IconUrl = "https://aptekazawiszy.pl/img/ident/cynamon-na-stole",
                },
                new IngredientPOCO
                {
                    Id = 2,
                    Name = "Aloe oil",
                    IconUrl = "https://5.imimg.com/data5/LV/XV/YR/SELLER-24094004/aloe-vera-oil.jpg",
                },
                new IngredientPOCO
                {
                    Id = 3,
                    Name = "Cocoa",
                    IconUrl = "https://www.shutterstock.com/image-photo/cocoa-ingredients-beans-fresh-pod-600nw-2263048751.jpg",
                },
                new IngredientPOCO
                {
                    Id = 4,
                    Name = "Turmeric",
                    IconUrl = "https://www.organicfacts.net/wp-content/uploads/2013/05/Turmeric-1.jpg",
                },
                new IngredientPOCO
                {
                    Id = 5,
                    Name = "Quinoa",
                    IconUrl = "https://www.verywellfit.com/thmb/UYfHq07-V-4zMNy8QSeKbEKTVwE=/3944x2958/filters:no_upscale():max_bytes(150000):strip_icc()/GettyImages-1130534199-c10e27a01a1145f1a85dc88b8fa9fc87.jpg",
                },
                new IngredientPOCO
                {
                    Id = 6,
                    Name = "Spirulina",
                    IconUrl = "https://www.healthline.com/hlcmsresource/images/topic_centers/2020-7/spirulina-powder-pinterest-1296x728-header.jpg",
                },
                new IngredientPOCO
                {
                    Id = 7,
                    Name = "Hemp Seeds",
                    IconUrl = "https://images-na.ssl-images-amazon.com/images/I/71zna2lq6rL._SL1500_.jpg",
                },
                new IngredientPOCO
                {
                    Id = 8,
                    Name = "Chia Seeds",
                    IconUrl = "https://images-na.ssl-images-amazon.com/images/I/81-u7t4GdGL._SL1500_.jpg",
                },
                new IngredientPOCO
                {
                    Id = 9,
                    Name = "Matcha Powder",
                    IconUrl = "https://www.thespruceeats.com/thmb/5xIGgEKcUWRHrXTnBxZ3FYgMJK8=/4494x2531/smart/filters:no_upscale()/matcha-powder-507284181-58adba215f9b58a3c9315b80.jpg",
                },
                new IngredientPOCO
                {
                    Id = 10,
                    Name = "Goji Berries",
                    IconUrl = "https://www.thespruceeats.com/thmb/dyFLswBswq4FV7qRRxdZZfxYFZs=/2733x2733/smart/filters:no_upscale()/GettyImages-529433302-5794ebe65f9b589aa94d7c95.jpg",
                },
                new IngredientPOCO
                {
                    Id = 11,
                    Name = "Maca Powder",
                    IconUrl = "https://cdn.shopify.com/s/files/1/0250/6972/2416/products/AdobeStock_244960664_1200x1200.jpg?v=1629458542",
                },
                new IngredientPOCO
                {
                    Id = 12,
                    Name = "Wheatgrass",
                    IconUrl = "https://www.thespruceeats.com/thmb/1pHhnqKY8bVqlkigqXcfymy86NU=/2735x2735/smart/filters:no_upscale()/GettyImages-1195962664-121f8b08c99f44b6ba7a6f665204a2bc.jpg",
                },
                new IngredientPOCO
                {
                    Id = 13,
                    Name = "Bee Pollen",
                    IconUrl = "https://images-na.ssl-images-amazon.com/images/I/61uvnYeNn5L._SL1000_.jpg",
                }
            ];
    }
}
