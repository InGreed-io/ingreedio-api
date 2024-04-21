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
                    IconUrl = "https://mlekovita.com.pl/media/cache/product_view/uploads/images/i3bRQpfKXVq01voDWA7x/8616-mleko-i-love-milk-3-5-3d.jpg",
                    Description = "Low-fat milk straight from the cow.",
                    CategoryId = 1,
                },
                new ProductPOCO
                {
                    Id = 2,
                    Name = "Almond Milk",
                    IconUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSD5j8AiAg7gBuP7NIndrWs67vpHiCm5IqbzxTHB3bFTw&s",
                    Description = "Dairy-free, unsweetened almond milk made from real almonds.",
                    CategoryId = 1,
                },
                new ProductPOCO
                {
                    Id = 3,
                    Name = "Oat Milk",
                    IconUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSrv2lqfti5o2EIh2x5JeH88de_UirKJH_KMLeYc_xGZQ&s",
                    Description = "Dairy-free, unsweetened oat milk made from oat.",
                    CategoryId = 1,
                },
                new ProductPOCO
                {
                  Id = 4,
                  Name = "Goat Milk",
                  IconUrl = "https://i5.walmartimages.com/asr/df1d6c29-246d-417e-be0c-04e380939d95_1.bf59e9a558a82e5041f1e9627cc4d8bb.png",
                  Description = "Milk from goats, known for its easier digestion for some.",
                  CategoryId = 1,
                },
                new ProductPOCO
                {
                  Id = 5,
                  Name = "Coconut Milk",
                  IconUrl = "https://shop.goya.com/cdn/shop/files/goya-021647-unsweetened_coconut_milk.png?v=1701110942",
                  Description = "Creamy, non-dairy milk made from the grated flesh of mature coconuts.",
                  CategoryId = 1,
                },
                new ProductPOCO
                {
                  Id = 6,
                  Name = "Soy Milk",
                  IconUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTCa5jVpo4iIUTjSqDSw8Yshk_01pyXDTXZN3LFOCXeGw&s",
                  Description = "Plant-based milk made from soybeans, a good source of protein.",
                  CategoryId = 1,
                },
                new ProductPOCO
                {
                  Id = 7,
                  Name = "Cashew Milk",
                  IconUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTIShBHK-kvnYi_M8MDkTImJq8VFmiixQDnTkS79lwUKQ&s",
                  Description = "Dairy-free milk made from cashews, known for its creamy texture.",
                  CategoryId = 1,
                },
                new ProductPOCO
                {
                  Id = 8,
                  Name = "Rice Milk",
                  IconUrl = "https://target.scene7.com/is/image/Target/GUEST_3c79e695-ec5e-417c-bc17-b0d1e7d38423?wid=488&hei=488&fmt=pjpeg",
                  Description = "Dairy-free milk made from ground rice, often hypoallergenic.",
                  CategoryId = 1,
                },
                new ProductPOCO
                {
                  Id = 9,
                  Name = "Lactose-Free Cow Milk",
                  IconUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQbbrL1hEdFlT3zanJyca_BhFUQHV_c61QH3hiPi7zveA&s",
                  Description = "Cow milk treated to remove lactose, suitable for those with lactose intolerance.",
                  CategoryId = 1,
                },
                new ProductPOCO
                {
                  Id = 10,
                  Name = "Chocolate Milk",
                  IconUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTTPq2ag-ADZAbWNGZrHU7UH-Y1tjn10BzSHdQUV09wpg&s",
                  Description = "Cow milk flavored with cocoa and sugar, a popular drink for children.",
                  CategoryId = 1,
                },
                new ProductPOCO
                {
                  Id = 11,
                  Name = "Strawberry Milk",
                  IconUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTVeD2cHeIOKWHkMyx45ronNBdyVmeO5ypTCsfvNK-Odw&s",
                  Description = "Cow milk flavored with strawberry and sugar, another popular flavored milk.",
                  CategoryId = 1,
                },
                new ProductPOCO
                {
                  Id = 12,
                  Name = "Vanilla Flavored Milk",
                  IconUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTNVYNhZg5Wyi4EoL83tRN6lyvCN9xTia1TghlhoFjHGw&s",
                  Description = "Cow milk flavored with vanilla extract and sugar, a classic flavored milk option.",
                  CategoryId = 1,
                }
            ];
    }
}
