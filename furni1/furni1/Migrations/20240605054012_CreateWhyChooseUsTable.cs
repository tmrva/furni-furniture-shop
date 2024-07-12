using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace furni1.Migrations
{
    /// <inheritdoc />
    public partial class CreateWhyChooseUsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WhyChooseUsId",
                table: "Benefits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WhyChooseUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhyChooseUs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Benefits_WhyChooseUsId",
                table: "Benefits",
                column: "WhyChooseUsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Benefits_WhyChooseUs_WhyChooseUsId",
                table: "Benefits",
                column: "WhyChooseUsId",
                principalTable: "WhyChooseUs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Benefits_WhyChooseUs_WhyChooseUsId",
                table: "Benefits");

            migrationBuilder.DropTable(
                name: "WhyChooseUs");

            migrationBuilder.DropIndex(
                name: "IX_Benefits_WhyChooseUsId",
                table: "Benefits");

            migrationBuilder.DropColumn(
                name: "WhyChooseUsId",
                table: "Benefits");
        }
    }
}
