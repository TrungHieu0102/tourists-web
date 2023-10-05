using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrungHieuTourists.Migrations
{
    /// <inheritdoc />
    public partial class RenameInvalidColumnTour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreategorySlug",
                table: "WEBTours",
                newName: "CategorySlug");

            migrationBuilder.RenameColumn(
                name: "CreategoryName",
                table: "WEBTours",
                newName: "CategoryName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategorySlug",
                table: "WEBTours",
                newName: "CreategorySlug");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "WEBTours",
                newName: "CreategoryName");
        }
    }
}
