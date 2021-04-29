using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class Migration012 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHistories_Transactions_TransactionId",
                table: "PurchaseHistories");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseHistories_TransactionId",
                table: "PurchaseHistories");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "PurchaseHistories");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "PurchaseHistories");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "PurchaseHistories",
                type: "decimal(14,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14, 2)");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "PurchaseHistories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "PurchaseHistories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "PurchaseHistories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchaseHistoryIndexId",
                table: "PurchaseHistories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PurchaseHistories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "PurchaseHistories",
                type: "decimal(14,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "PurchaseHistoryIndexs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(nullable: false),
                    PaymentId = table.Column<int>(nullable: false),
                    ShippingId = table.Column<int>(nullable: false),
                    UserPropertyId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    ShippingAddress = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    DateTransactionDone = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseHistoryIndexs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistories_PurchaseHistoryIndexId",
                table: "PurchaseHistories",
                column: "PurchaseHistoryIndexId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHistories_PurchaseHistoryIndexs_PurchaseHistoryIndexId",
                table: "PurchaseHistories",
                column: "PurchaseHistoryIndexId",
                principalTable: "PurchaseHistoryIndexs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHistories_PurchaseHistoryIndexs_PurchaseHistoryIndexId",
                table: "PurchaseHistories");

            migrationBuilder.DropTable(
                name: "PurchaseHistoryIndexs");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseHistories_PurchaseHistoryIndexId",
                table: "PurchaseHistories");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "PurchaseHistories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "PurchaseHistories");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "PurchaseHistories");

            migrationBuilder.DropColumn(
                name: "PurchaseHistoryIndexId",
                table: "PurchaseHistories");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PurchaseHistories");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "PurchaseHistories");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "PurchaseHistories",
                type: "decimal(14, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)");

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "PurchaseHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "PurchaseHistories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistories_TransactionId",
                table: "PurchaseHistories",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHistories_Transactions_TransactionId",
                table: "PurchaseHistories",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
