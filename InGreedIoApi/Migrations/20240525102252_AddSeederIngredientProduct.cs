using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InGreedIoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSeederIngredientProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "IconUrl", "Name", "PreferencePOCOId", "PreferencePOCOId1" },
                values: new object[,]
                {
                    { 1, "https://aptekazawiszy.pl/img/ident/cynamon-na-stole", "Cinamon", null, null },
                    { 2, "https://5.imimg.com/data5/LV/XV/YR/SELLER-24094004/aloe-vera-oil.jpg", "Aloe oil", null, null },
                    { 3, "https://www.shutterstock.com/image-photo/cocoa-ingredients-beans-fresh-pod-600nw-2263048751.jpg", "Cocoa", null, null },
                    { 4, "https://www.organicfacts.net/wp-content/uploads/2013/05/Turmeric-1.jpg", "Turmeric", null, null },
                    { 5, "https://www.verywellfit.com/thmb/UYfHq07-V-4zMNy8QSeKbEKTVwE=/3944x2958/filters:no_upscale():max_bytes(150000):strip_icc()/GettyImages-1130534199-c10e27a01a1145f1a85dc88b8fa9fc87.jpg", "Quinoa", null, null },
                    { 6, "https://www.healthline.com/hlcmsresource/images/topic_centers/2020-7/spirulina-powder-pinterest-1296x728-header.jpg", "Spirulina", null, null },
                    { 7, "https://images-na.ssl-images-amazon.com/images/I/71zna2lq6rL._SL1500_.jpg", "Hemp Seeds", null, null },
                    { 8, "https://images-na.ssl-images-amazon.com/images/I/81-u7t4GdGL._SL1500_.jpg", "Chia Seeds", null, null },
                    { 9, "https://www.thespruceeats.com/thmb/5xIGgEKcUWRHrXTnBxZ3FYgMJK8=/4494x2531/smart/filters:no_upscale()/matcha-powder-507284181-58adba215f9b58a3c9315b80.jpg", "Matcha Powder", null, null },
                    { 10, "https://www.thespruceeats.com/thmb/dyFLswBswq4FV7qRRxdZZfxYFZs=/2733x2733/smart/filters:no_upscale()/GettyImages-529433302-5794ebe65f9b589aa94d7c95.jpg", "Goji Berries", null, null },
                    { 11, "https://cdn.shopify.com/s/files/1/0250/6972/2416/products/AdobeStock_244960664_1200x1200.jpg?v=1629458542", "Maca Powder", null, null },
                    { 12, "https://www.thespruceeats.com/thmb/1pHhnqKY8bVqlkigqXcfymy86NU=/2735x2735/smart/filters:no_upscale()/GettyImages-1195962664-121f8b08c99f44b6ba7a6f665204a2bc.jpg", "Wheatgrass", null, null },
                    { 13, "https://images-na.ssl-images-amazon.com/images/I/61uvnYeNn5L._SL1000_.jpg", "Bee Pollen", null, null },
                    { 14, "", "Oat", null, null },
                    { 15, "", "Almond", null, null },
                    { 16, "", "Strawberry", null, null },
                    { 17, "", "Cashew", null, null },
                    { 18, "", "Coconut", null, null },
                    { 19, "", "Soy", null, null },
                    { 20, "", "Vanilla", null, null },
                    { 21, "", "Rice", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "IconUrl",
                value: "https://mlekovita.com.pl/media/cache/product_view/uploads/images/i3bRQpfKXVq01voDWA7x/8616-mleko-i-love-milk-3-5-3d.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "IconUrl",
                value: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSD5j8AiAg7gBuP7NIndrWs67vpHiCm5IqbzxTHB3bFTw&s");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "IconUrl",
                value: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSrv2lqfti5o2EIh2x5JeH88de_UirKJH_KMLeYc_xGZQ&s");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "FeaturingId", "IconUrl", "Name", "ProducerId" },
                values: new object[,]
                {
                    { 4, 1, "Milk from goats, known for its easier digestion for some.", null, "https://i5.walmartimages.com/asr/df1d6c29-246d-417e-be0c-04e380939d95_1.bf59e9a558a82e5041f1e9627cc4d8bb.png", "Goat Milk", null },
                    { 5, 1, "Creamy, non-dairy milk made from the grated flesh of mature coconuts.", null, "https://shop.goya.com/cdn/shop/files/goya-021647-unsweetened_coconut_milk.png?v=1701110942", "Coconut Milk", null },
                    { 6, 1, "Plant-based milk made from soybeans, a good source of protein.", null, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTCa5jVpo4iIUTjSqDSw8Yshk_01pyXDTXZN3LFOCXeGw&s", "Soy Milk", null },
                    { 7, 1, "Dairy-free milk made from cashews, known for its creamy texture.", null, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTIShBHK-kvnYi_M8MDkTImJq8VFmiixQDnTkS79lwUKQ&s", "Cashew Milk", null },
                    { 8, 1, "Dairy-free milk made from ground rice, often hypoallergenic.", null, "https://target.scene7.com/is/image/Target/GUEST_3c79e695-ec5e-417c-bc17-b0d1e7d38423?wid=488&hei=488&fmt=pjpeg", "Rice Milk", null },
                    { 9, 1, "Cow milk treated to remove lactose, suitable for those with lactose intolerance.", null, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQbbrL1hEdFlT3zanJyca_BhFUQHV_c61QH3hiPi7zveA&s", "Lactose-Free Cow Milk", null },
                    { 10, 1, "Cow milk flavored with cocoa and sugar, a popular drink for children.", null, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTTPq2ag-ADZAbWNGZrHU7UH-Y1tjn10BzSHdQUV09wpg&s", "Chocolate Milk", null },
                    { 11, 1, "Cow milk flavored with strawberry and sugar, another popular flavored milk.", null, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTVeD2cHeIOKWHkMyx45ronNBdyVmeO5ypTCsfvNK-Odw&s", "Strawberry Milk", null },
                    { 12, 1, "Cow milk flavored with vanilla extract and sugar, a classic flavored milk option.", null, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTNVYNhZg5Wyi4EoL83tRN6lyvCN9xTia1TghlhoFjHGw&s", "Vanilla Flavored Milk", null }
                });

            migrationBuilder.InsertData(
                table: "IngredientPOCOProductPOCO",
                columns: new[] { "IngredientsId", "ProductsId" },
                values: new object[,]
                {
                    { 1, 11 },
                    { 3, 1 },
                    { 3, 10 },
                    { 4, 3 },
                    { 6, 6 },
                    { 14, 3 },
                    { 15, 2 },
                    { 16, 11 },
                    { 17, 7 },
                    { 18, 5 },
                    { 19, 6 },
                    { 20, 12 },
                    { 21, 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IngredientPOCOProductPOCO",
                keyColumns: new[] { "IngredientsId", "ProductsId" },
                keyValues: new object[] { 1, 11 });

            migrationBuilder.DeleteData(
                table: "IngredientPOCOProductPOCO",
                keyColumns: new[] { "IngredientsId", "ProductsId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "IngredientPOCOProductPOCO",
                keyColumns: new[] { "IngredientsId", "ProductsId" },
                keyValues: new object[] { 3, 10 });

            migrationBuilder.DeleteData(
                table: "IngredientPOCOProductPOCO",
                keyColumns: new[] { "IngredientsId", "ProductsId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "IngredientPOCOProductPOCO",
                keyColumns: new[] { "IngredientsId", "ProductsId" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "IngredientPOCOProductPOCO",
                keyColumns: new[] { "IngredientsId", "ProductsId" },
                keyValues: new object[] { 14, 3 });

            migrationBuilder.DeleteData(
                table: "IngredientPOCOProductPOCO",
                keyColumns: new[] { "IngredientsId", "ProductsId" },
                keyValues: new object[] { 15, 2 });

            migrationBuilder.DeleteData(
                table: "IngredientPOCOProductPOCO",
                keyColumns: new[] { "IngredientsId", "ProductsId" },
                keyValues: new object[] { 16, 11 });

            migrationBuilder.DeleteData(
                table: "IngredientPOCOProductPOCO",
                keyColumns: new[] { "IngredientsId", "ProductsId" },
                keyValues: new object[] { 17, 7 });

            migrationBuilder.DeleteData(
                table: "IngredientPOCOProductPOCO",
                keyColumns: new[] { "IngredientsId", "ProductsId" },
                keyValues: new object[] { 18, 5 });

            migrationBuilder.DeleteData(
                table: "IngredientPOCOProductPOCO",
                keyColumns: new[] { "IngredientsId", "ProductsId" },
                keyValues: new object[] { 19, 6 });

            migrationBuilder.DeleteData(
                table: "IngredientPOCOProductPOCO",
                keyColumns: new[] { "IngredientsId", "ProductsId" },
                keyValues: new object[] { 20, 12 });

            migrationBuilder.DeleteData(
                table: "IngredientPOCOProductPOCO",
                keyColumns: new[] { "IngredientsId", "ProductsId" },
                keyValues: new object[] { 21, 8 });

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "IconUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "IconUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "IconUrl",
                value: "");
        }
    }
}
