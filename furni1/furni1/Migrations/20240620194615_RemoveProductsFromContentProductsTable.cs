using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace furni1.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProductsFromContentProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ContentProducts_ContentProductId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ContentProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ContentProductId",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContentProductId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ContentProductId",
                table: "Products",
                column: "ContentProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ContentProducts_ContentProductId",
                table: "Products",
                column: "ContentProductId",
                principalTable: "ContentProducts",
                principalColumn: "Id");
        }
    }
}
