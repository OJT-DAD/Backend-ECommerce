using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class Migration004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailableShipment",
                table: "AvailableShipment");

            migrationBuilder.RenameTable(
                name: "AvailableShipment",
                newName: "AvailableShipments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailableShipments",
                table: "AvailableShipments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AvailableBanks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableBanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvailableShipmentId = table.Column<int>(nullable: false),
                    UserPropertyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipments_AvailableShipments_AvailableShipmentId",
                        column: x => x.AvailableShipmentId,
                        principalTable: "AvailableShipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shipments_UserProperties_UserPropertyId",
                        column: x => x.UserPropertyId,
                        principalTable: "UserProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvailableBankId = table.Column<int>(nullable: false),
                    BankAccountNumber = table.Column<string>(nullable: true),
                    UserPropertyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AvailableBanks_AvailableBankId",
                        column: x => x.AvailableBankId,
                        principalTable: "AvailableBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_UserProperties_UserPropertyId",
                        column: x => x.UserPropertyId,
                        principalTable: "UserProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AvailableBankId",
                table: "Payments",
                column: "AvailableBankId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserPropertyId",
                table: "Payments",
                column: "UserPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_AvailableShipmentId",
                table: "Shipments",
                column: "AvailableShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_UserPropertyId",
                table: "Shipments",
                column: "UserPropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "AvailableBanks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailableShipments",
                table: "AvailableShipments");

            migrationBuilder.RenameTable(
                name: "AvailableShipments",
                newName: "AvailableShipment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailableShipment",
                table: "AvailableShipment",
                column: "Id");
        }
    }
}
