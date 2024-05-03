using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitTrade.Migrations
{
    /// <inheritdoc />
    public partial class UserTransactionRelationship2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Transactions_Users_ApplicationUserId",
            //    table: "Transactions");
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            //migrationBuilder.AlterColumn<string>(
            //    name: "ApplicationUserId",
            //    table: "Transactions",
            //    type: "nvarchar(450)",
            //    nullable: false,
            //    defaultValue: "",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)",
            //    oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_ApplicationUserId",
                table: "Transactions",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Transactions_Users_ApplicationUserId",
            //    table: "Transactions");

            //migrationBuilder.AlterColumn<string>(
            //    name: "ApplicationUserId",
            //    table: "Transactions",
            //    type: "nvarchar(450)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_ApplicationUserId",
                table: "Transactions",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
