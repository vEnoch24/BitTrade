using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitTrade.Migrations
{
    /// <inheritdoc />
    public partial class Rollback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Transactions_Users_ApplicationUserId",
            //    table: "Transactions");

            //migrationBuilder.DropTable(
            //    name: "Users");

            //migrationBuilder.DropIndex(
            //    name: "IX_Transactions_ApplicationUserId",
            //    table: "Transactions");

            //migrationBuilder.DropColumn(
            //    name: "ApplicationUserId",
            //    table: "Transactions");

            //migrationBuilder.DropColumn(
            //    name: "UserId",
            //    table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "ApplicationUserId",
            //    table: "Transactions",
            //    type: "nvarchar(450)",
            //    nullable: true);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "UserId",
            //    table: "Transactions",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        AccessFailedCount = table.Column<int>(type: "int", nullable: false),
            //        ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //        LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
            //        LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //        SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
            //        UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Users", x => x.Id);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Transactions_ApplicationUserId",
            //    table: "Transactions",
            //    column: "ApplicationUserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Transactions_Users_ApplicationUserId",
            //    table: "Transactions",
            //    column: "ApplicationUserId",
            //    principalTable: "Users",
            //    principalColumn: "Id");
        }
    }
}
