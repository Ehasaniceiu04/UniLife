using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class dersChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BolumId",
                table: "Derss",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FakulteId",
                table: "Derss",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "Derss",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BolumId",
                table: "DersAcilans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FakulteId",
                table: "DersAcilans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Derss_BolumId",
                table: "Derss",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_Derss_FakulteId",
                table: "Derss",
                column: "FakulteId");

            migrationBuilder.CreateIndex(
                name: "IX_Derss_ProgramId",
                table: "Derss",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilans_BolumId",
                table: "DersAcilans",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilans_FakulteId",
                table: "DersAcilans",
                column: "FakulteId");

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilans_Bolums_BolumId",
                table: "DersAcilans",
                column: "BolumId",
                principalTable: "Bolums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilans_Fakultes_FakulteId",
                table: "DersAcilans",
                column: "FakulteId",
                principalTable: "Fakultes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_Bolums_BolumId",
                table: "Derss",
                column: "BolumId",
                principalTable: "Bolums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_Fakultes_FakulteId",
                table: "Derss",
                column: "FakulteId",
                principalTable: "Fakultes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_Programs_ProgramId",
                table: "Derss",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilans_Bolums_BolumId",
                table: "DersAcilans");

            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilans_Fakultes_FakulteId",
                table: "DersAcilans");

            migrationBuilder.DropForeignKey(
                name: "FK_Derss_Bolums_BolumId",
                table: "Derss");

            migrationBuilder.DropForeignKey(
                name: "FK_Derss_Fakultes_FakulteId",
                table: "Derss");

            migrationBuilder.DropForeignKey(
                name: "FK_Derss_Programs_ProgramId",
                table: "Derss");

            migrationBuilder.DropIndex(
                name: "IX_Derss_BolumId",
                table: "Derss");

            migrationBuilder.DropIndex(
                name: "IX_Derss_FakulteId",
                table: "Derss");

            migrationBuilder.DropIndex(
                name: "IX_Derss_ProgramId",
                table: "Derss");

            migrationBuilder.DropIndex(
                name: "IX_DersAcilans_BolumId",
                table: "DersAcilans");

            migrationBuilder.DropIndex(
                name: "IX_DersAcilans_FakulteId",
                table: "DersAcilans");

            migrationBuilder.DropColumn(
                name: "BolumId",
                table: "Derss");

            migrationBuilder.DropColumn(
                name: "FakulteId",
                table: "Derss");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Derss");

            migrationBuilder.DropColumn(
                name: "BolumId",
                table: "DersAcilans");

            migrationBuilder.DropColumn(
                name: "FakulteId",
                table: "DersAcilans");
        }
    }
}
