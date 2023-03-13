using Microsoft.EntityFrameworkCore.Migrations;

namespace Electricity.CRM.API.Migrations
{
    public partial class elecricitybiller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectricityBiller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommercialUserId = table.Column<int>(type: "int", nullable: true),
                    ResidentialUserId = table.Column<int>(type: "int", nullable: true),
                    FlatUserId = table.Column<int>(type: "int", nullable: true),
                    FactoryUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricityBiller", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectricityBiller_ElectricityUserCommercial_CommercialUserId",
                        column: x => x.CommercialUserId,
                        principalTable: "ElectricityUserCommercial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectricityBiller_ElectricityUserFactory_FactoryUserId",
                        column: x => x.FactoryUserId,
                        principalTable: "ElectricityUserFactory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectricityBiller_ElectricityUserFlat_FlatUserId",
                        column: x => x.FlatUserId,
                        principalTable: "ElectricityUserFlat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectricityBiller_ElectricityUserResidential_ResidentialUserId",
                        column: x => x.ResidentialUserId,
                        principalTable: "ElectricityUserResidential",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectricityBiller_CommercialUserId",
                table: "ElectricityBiller",
                column: "CommercialUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectricityBiller_FactoryUserId",
                table: "ElectricityBiller",
                column: "FactoryUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectricityBiller_FlatUserId",
                table: "ElectricityBiller",
                column: "FlatUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectricityBiller_ResidentialUserId",
                table: "ElectricityBiller",
                column: "ResidentialUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectricityBiller");
        }
    }
}
