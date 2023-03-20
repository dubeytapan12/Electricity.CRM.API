using Microsoft.EntityFrameworkCore.Migrations;

namespace Electricity.CRM.API.Migrations
{
    public partial class addedppttables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TechnologyEnablement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobiles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Background = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnologyEnablement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechnologyEnablementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_TechnologyEnablement_TechnologyEnablementId",
                        column: x => x.TechnologyEnablementId,
                        principalTable: "TechnologyEnablement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationOrCertification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechnologyEnablementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationOrCertification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationOrCertification_TechnologyEnablement_TechnologyEnablementId",
                        column: x => x.TechnologyEnablementId,
                        principalTable: "TechnologyEnablement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experience",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperienceDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechnologyEnablementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experience_TechnologyEnablement_TechnologyEnablementId",
                        column: x => x.TechnologyEnablementId,
                        principalTable: "TechnologyEnablement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechnologyEnablementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Languages_TechnologyEnablement_TechnologyEnablementId",
                        column: x => x.TechnologyEnablementId,
                        principalTable: "TechnologyEnablement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    skill = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechnologyEnablementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_TechnologyEnablement_TechnologyEnablementId",
                        column: x => x.TechnologyEnablementId,
                        principalTable: "TechnologyEnablement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_TechnologyEnablementId",
                table: "Client",
                column: "TechnologyEnablementId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationOrCertification_TechnologyEnablementId",
                table: "EducationOrCertification",
                column: "TechnologyEnablementId");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_TechnologyEnablementId",
                table: "Experience",
                column: "TechnologyEnablementId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_TechnologyEnablementId",
                table: "Languages",
                column: "TechnologyEnablementId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_TechnologyEnablementId",
                table: "Skills",
                column: "TechnologyEnablementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "EducationOrCertification");

            migrationBuilder.DropTable(
                name: "Experience");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "TechnologyEnablement");
        }
    }
}
