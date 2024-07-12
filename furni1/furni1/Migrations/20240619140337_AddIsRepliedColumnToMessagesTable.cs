using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace furni1.Migrations
{
    /// <inheritdoc />
    public partial class AddIsRepliedColumnToMessagesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReplied",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReplied",
                table: "Messages");
        }
    }
}
