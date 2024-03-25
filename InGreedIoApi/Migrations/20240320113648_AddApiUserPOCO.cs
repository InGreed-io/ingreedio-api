using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InGreedIoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddApiUserPOCO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiUserProductPOCO");

            migrationBuilder.CreateTable(
                name: "ApiUserPOCOProductPOCO",
                columns: table => new
                {
                    FavouriteById = table.Column<string>(type: "text", nullable: false),
                    FavouriteProductsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiUserPOCOProductPOCO", x => new { x.FavouriteById, x.FavouriteProductsId });
                    table.ForeignKey(
                        name: "FK_ApiUserPOCOProductPOCO_AspNetUsers_FavouriteById",
                        column: x => x.FavouriteById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApiUserPOCOProductPOCO_Products_FavouriteProductsId",
                        column: x => x.FavouriteProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiUserPOCOProductPOCO_FavouriteProductsId",
                table: "ApiUserPOCOProductPOCO",
                column: "FavouriteProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiUserPOCOProductPOCO");

            migrationBuilder.CreateTable(
                name: "ApiUserProductPOCO",
                columns: table => new
                {
                    FavouriteById = table.Column<string>(type: "text", nullable: false),
                    FavouriteProductsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiUserProductPOCO", x => new { x.FavouriteById, x.FavouriteProductsId });
                    table.ForeignKey(
                        name: "FK_ApiUserProductPOCO_AspNetUsers_FavouriteById",
                        column: x => x.FavouriteById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApiUserProductPOCO_Products_FavouriteProductsId",
                        column: x => x.FavouriteProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiUserProductPOCO_FavouriteProductsId",
                table: "ApiUserProductPOCO",
                column: "FavouriteProductsId");
        }
    }
}
