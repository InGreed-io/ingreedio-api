using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InGreedIoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPreferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreferencePOCOId",
                table: "Ingredients",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreferencePOCOId1",
                table: "Ingredients",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Preferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_PreferencePOCOId",
                table: "Ingredients",
                column: "PreferencePOCOId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_PreferencePOCOId1",
                table: "Ingredients",
                column: "PreferencePOCOId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Preferences_PreferencePOCOId",
                table: "Ingredients",
                column: "PreferencePOCOId",
                principalTable: "Preferences",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Preferences_PreferencePOCOId1",
                table: "Ingredients",
                column: "PreferencePOCOId1",
                principalTable: "Preferences",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Preferences_PreferencePOCOId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Preferences_PreferencePOCOId1",
                table: "Ingredients");

            migrationBuilder.DropTable(
                name: "Preferences");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_PreferencePOCOId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_PreferencePOCOId1",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "PreferencePOCOId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "PreferencePOCOId1",
                table: "Ingredients");
        }
    }
}
