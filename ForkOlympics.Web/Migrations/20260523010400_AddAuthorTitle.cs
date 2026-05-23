using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForkOlympics.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Authors",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Authors");
        }
    }
}
