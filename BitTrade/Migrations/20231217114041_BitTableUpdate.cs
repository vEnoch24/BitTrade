using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitTrade.Migrations
{
    /// <inheritdoc />
    public partial class BitTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BitcoinAddress",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "BitcoinAmount",
                table: "Transactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BitcoinAddress",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BitcoinAmount",
                table: "Transactions");
        }
    }
}
