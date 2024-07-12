using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace furni1.Migrations
{
    /// <inheritdoc />
    public partial class AddMiniColumnsToWhyChooseUsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Benefits_WhyChooseUs_WhyChooseUsId",
                table: "Benefits");

            migrationBuilder.DropIndex(
                name: "IX_Benefits_WhyChooseUsId",
                table: "Benefits");

            migrationBuilder.DropColumn(
                name: "WhyChooseUsId",
                table: "Benefits");

            migrationBuilder.AddColumn<string>(
                name: "MiniDescription1",
                table: "WhyChooseUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiniDescription2",
                table: "WhyChooseUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiniDescription3",
                table: "WhyChooseUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiniDescription4",
                table: "WhyChooseUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiniIcon1",
                table: "WhyChooseUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiniIcon2",
                table: "WhyChooseUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiniIcon3",
                table: "WhyChooseUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiniIcon4",
                table: "WhyChooseUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiniTitle1",
                table: "WhyChooseUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiniTitle2",
                table: "WhyChooseUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiniTitle3",
                table: "WhyChooseUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiniTitle4",
                table: "WhyChooseUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiniDescription1",
                table: "WhyChooseUs");

            migrationBuilder.DropColumn(
                name: "MiniDescription2",
                table: "WhyChooseUs");

            migrationBuilder.DropColumn(
                name: "MiniDescription3",
                table: "WhyChooseUs");

            migrationBuilder.DropColumn(
                name: "MiniDescription4",
                table: "WhyChooseUs");

            migrationBuilder.DropColumn(
                name: "MiniIcon1",
                table: "WhyChooseUs");

            migrationBuilder.DropColumn(
                name: "MiniIcon2",
                table: "WhyChooseUs");

            migrationBuilder.DropColumn(
                name: "MiniIcon3",
                table: "WhyChooseUs");

            migrationBuilder.DropColumn(
                name: "MiniIcon4",
                table: "WhyChooseUs");

            migrationBuilder.DropColumn(
                name: "MiniTitle1",
                table: "WhyChooseUs");

            migrationBuilder.DropColumn(
                name: "MiniTitle2",
                table: "WhyChooseUs");

            migrationBuilder.DropColumn(
                name: "MiniTitle3",
                table: "WhyChooseUs");

            migrationBuilder.DropColumn(
                name: "MiniTitle4",
                table: "WhyChooseUs");

            migrationBuilder.AddColumn<int>(
                name: "WhyChooseUsId",
                table: "Benefits",
                type: "int",
                nullable: true);

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
    }
}
