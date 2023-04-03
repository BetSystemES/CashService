using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashService.DataAccess.Migrations
{
    public partial class AddCashAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CashAmount",
                table: "TransactionProfile",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "RowVersion",
                table: "TransactionProfile",
                type: "bigint",
                rowVersion: true,
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashAmount",
                table: "TransactionProfile");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "TransactionProfile");
        }
    }
}
