using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class Migration017 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "PurchaseHistories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "PurchaseHistories");
        }
    }
}
