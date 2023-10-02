using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrungHieuTourists.Migrations
{
    /// <inheritdoc />
    public partial class addSellprice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SellPrice",
                table: "WEBTours",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellPrice",
                table: "WEBTours");
        }
    }
}
