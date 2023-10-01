using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrungHieuTourists.Migrations
{
    /// <inheritdoc />
    public partial class FixNameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WEB PromotionCountries",
                table: "WEB PromotionCountries");

            migrationBuilder.RenameTable(
                name: "WEB PromotionCountries",
                newName: "WEBPromotionCountries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WEBPromotionCountries",
                table: "WEBPromotionCountries",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WEBPromotionCountries",
                table: "WEBPromotionCountries");

            migrationBuilder.RenameTable(
                name: "WEBPromotionCountries",
                newName: "WEB PromotionCountries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WEB PromotionCountries",
                table: "WEB PromotionCountries",
                column: "Id");
        }
    }
}
