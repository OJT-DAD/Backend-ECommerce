using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class Migration011 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(nullable: true),
                    ShippingAddress = table.Column<string>(nullable: true),
                    TransactionIndexId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalDatas_TransactionIndexs_TransactionIndexId",
                        column: x => x.TransactionIndexId,
                        principalTable: "TransactionIndexs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalDatas_TransactionIndexId",
                table: "AdditionalDatas",
                column: "TransactionIndexId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalDatas");
        }
    }
}
