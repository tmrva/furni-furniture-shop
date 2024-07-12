using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace furni1.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeactiveColumnToProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "Testimonials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "TeamMembers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "Testimonials");

            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "Products");
        }
    }
}
