using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class Migration007 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Products_ProductId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ProductId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TotalTransactionPrice",
                table: "TransactionIndexs");

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "TransactionIndexs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShipmentId",
                table: "TransactionIndexs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "TransactionIndexs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionIndexs_StoreId",
                table: "TransactionIndexs",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionIndexs_Stores_StoreId",
                table: "TransactionIndexs",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionIndexs_Stores_StoreId",
                table: "TransactionIndexs");

            migrationBuilder.DropIndex(
                name: "IX_TransactionIndexs_StoreId",
                table: "TransactionIndexs");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "TransactionIndexs");

            migrationBuilder.DropColumn(
                name: "ShipmentId",
                table: "TransactionIndexs");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "TransactionIndexs");

            migrationBuilder.AddColumn<int>(
                name: "TotalTransactionPrice",
                table: "TransactionIndexs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ProductId",
                table: "Transactions",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Products_ProductId",
                table: "Transactions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
