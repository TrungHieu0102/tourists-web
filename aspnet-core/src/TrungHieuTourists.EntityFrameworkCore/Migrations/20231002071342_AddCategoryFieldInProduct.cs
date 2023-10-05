using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrungHieuTourists.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryFieldInProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreategoryName",
                table: "WEBTours",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreategorySlug",
                table: "WEBTours",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreategoryName",
                table: "WEBTours");

            migrationBuilder.DropColumn(
                name: "CreategorySlug",
                table: "WEBTours");
        }
    }
}
