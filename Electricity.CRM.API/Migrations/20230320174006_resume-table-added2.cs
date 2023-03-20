using Microsoft.EntityFrameworkCore.Migrations;

namespace Electricity.CRM.API.Migrations
{
    public partial class resumetableadded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resume_Resume_ResumeId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_Resume_ResumeId",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "Resume");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResumeId",
                table: "Resume",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resume_ResumeId",
                table: "Resume",
                column: "ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_Resume_ResumeId",
                table: "Resume",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
