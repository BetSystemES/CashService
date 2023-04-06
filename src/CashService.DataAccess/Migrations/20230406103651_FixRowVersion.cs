using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashService.DataAccess.Migrations
{
    public partial class FixRowVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "TransactionProfile",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "xmin",
                table: "TransactionProfile");
        }
    }
}
