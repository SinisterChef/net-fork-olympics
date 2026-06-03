using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForkOlympics.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorProfileFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "Authors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "Authors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsGuestAuthor",
                table: "Authors",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Education",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "IsGuestAuthor",
                table: "Authors");
        }
    }
}
