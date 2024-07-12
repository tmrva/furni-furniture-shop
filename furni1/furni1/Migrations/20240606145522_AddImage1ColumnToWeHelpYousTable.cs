using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace furni1.Migrations
{
    /// <inheritdoc />
    public partial class AddImage1ColumnToWeHelpYousTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Images",
                table: "WeHelpYous",
                newName: "Image3");

            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "WeHelpYous",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "WeHelpYous",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image1",
                table: "WeHelpYous");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "WeHelpYous");

            migrationBuilder.RenameColumn(
                name: "Image3",
                table: "WeHelpYous",
                newName: "Images");
        }
    }
}
