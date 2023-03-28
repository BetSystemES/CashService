using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashService.DataAccess.Migrations
{
    public partial class AddTransactionDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_TransactionProfile_TransactionProfileId",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "TransactionProfile",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TransactionProfileId",
                table: "Transaction",
                newName: "ProfileId");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Transaction",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_TransactionProfileId",
                table: "Transaction",
                newName: "IX_Transaction_ProfileId");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Date",
                table: "Transaction",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_TransactionProfile_ProfileId",
                table: "Transaction",
                column: "ProfileId",
                principalTable: "TransactionProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_TransactionProfile_ProfileId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TransactionProfile",
                newName: "ProfileId");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "Transaction",
                newName: "TransactionProfileId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Transaction",
                newName: "TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_ProfileId",
                table: "Transaction",
                newName: "IX_Transaction_TransactionProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_TransactionProfile_TransactionProfileId",
                table: "Transaction",
                column: "TransactionProfileId",
                principalTable: "TransactionProfile",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
