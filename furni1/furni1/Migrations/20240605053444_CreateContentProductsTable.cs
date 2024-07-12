using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace furni1.Migrations
{
    /// <inheritdoc />
    public partial class CreateContentProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContentProductId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContentProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentProducts", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ContentProducts_ContentProductId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ContentProducts");

            migrationBuilder.DropIndex(
                name: "IX_Products_ContentProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ContentProductId",
                table: "Products");
        }
    }
}
