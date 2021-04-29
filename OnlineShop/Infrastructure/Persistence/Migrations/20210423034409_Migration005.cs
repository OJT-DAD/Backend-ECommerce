using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class Migration005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_UserProperties_UserPropertyId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_UserProperties_UserPropertyId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_UserPropertyId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_UserPropertyId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UserPropertyId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "UserPropertyId",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Shipments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Payments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_StoreId",
                table: "Shipments",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StoreId",
                table: "Payments",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Stores_StoreId",
                table: "Payments",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Stores_StoreId",
                table: "Shipments",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Stores_StoreId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Stores_StoreId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_StoreId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_StoreId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "UserPropertyId",
                table: "Shipments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserPropertyId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_UserPropertyId",
                table: "Shipments",
                column: "UserPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserPropertyId",
                table: "Payments",
                column: "UserPropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_UserProperties_UserPropertyId",
                table: "Payments",
                column: "UserPropertyId",
                principalTable: "UserProperties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_UserProperties_UserPropertyId",
                table: "Shipments",
                column: "UserPropertyId",
                principalTable: "UserProperties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
