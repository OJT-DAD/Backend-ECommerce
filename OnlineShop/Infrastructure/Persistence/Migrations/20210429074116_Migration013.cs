using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class Migration013 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalDatas_TransactionIndexs_TransactionIndexId",
                table: "AdditionalDatas");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalDatas_TransactionIndexId",
                table: "AdditionalDatas");

            migrationBuilder.DropColumn(
                name: "TransactionIndexId",
                table: "AdditionalDatas");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "TransactionIndexs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "TransactionIndexs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalResult",
                table: "NewSellers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateApprovalResult",
                table: "NewSellers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CartIndexId",
                table: "AdditionalDatas",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "TransactionIndexs");

            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "TransactionIndexs");

            migrationBuilder.DropColumn(
                name: "ApprovalResult",
                table: "NewSellers");

            migrationBuilder.DropColumn(
                name: "DateApprovalResult",
                table: "NewSellers");

            migrationBuilder.DropColumn(
                name: "CartIndexId",
                table: "AdditionalDatas");

            migrationBuilder.AddColumn<int>(
                name: "TransactionIndexId",
                table: "AdditionalDatas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalDatas_TransactionIndexId",
                table: "AdditionalDatas",
                column: "TransactionIndexId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalDatas_TransactionIndexs_TransactionIndexId",
                table: "AdditionalDatas",
                column: "TransactionIndexId",
                principalTable: "TransactionIndexs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
