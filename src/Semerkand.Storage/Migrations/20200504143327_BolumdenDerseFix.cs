using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class BolumdenDerseFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BolumId",
                table: "Programs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "Mufredats",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MufredatID",
                table: "Derss",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FakulteId",
                table: "Bolums",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Programs_BolumId",
                table: "Programs",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_Mufredats_ProgramId",
                table: "Mufredats",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Derss_MufredatID",
                table: "Derss",
                column: "MufredatID");

            migrationBuilder.CreateIndex(
                name: "IX_Bolums_FakulteId",
                table: "Bolums",
                column: "FakulteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bolums_Fakultes_FakulteId",
                table: "Bolums",
                column: "FakulteId",
                principalTable: "Fakultes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_Mufredats_MufredatID",
                table: "Derss",
                column: "MufredatID",
                principalTable: "Mufredats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mufredats_Programs_ProgramId",
                table: "Mufredats",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Bolums_BolumId",
                table: "Programs",
                column: "BolumId",
                principalTable: "Bolums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolums_Fakultes_FakulteId",
                table: "Bolums");

            migrationBuilder.DropForeignKey(
                name: "FK_Derss_Mufredats_MufredatID",
                table: "Derss");

            migrationBuilder.DropForeignKey(
                name: "FK_Mufredats_Programs_ProgramId",
                table: "Mufredats");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Bolums_BolumId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Programs_BolumId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Mufredats_ProgramId",
                table: "Mufredats");

            migrationBuilder.DropIndex(
                name: "IX_Derss_MufredatID",
                table: "Derss");

            migrationBuilder.DropIndex(
                name: "IX_Bolums_FakulteId",
                table: "Bolums");

            migrationBuilder.DropColumn(
                name: "BolumId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Mufredats");

            migrationBuilder.DropColumn(
                name: "MufredatID",
                table: "Derss");

            migrationBuilder.DropColumn(
                name: "FakulteId",
                table: "Bolums");
        }
    }
}
