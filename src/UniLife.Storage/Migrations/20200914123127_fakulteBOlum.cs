using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class fakulteBOlum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BolumId",
                table: "Akademisyens",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FakulteId",
                table: "Akademisyens",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Akademisyens_BolumId",
                table: "Akademisyens",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_Akademisyens_FakulteId",
                table: "Akademisyens",
                column: "FakulteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Akademisyens_Bolums_BolumId",
                table: "Akademisyens",
                column: "BolumId",
                principalTable: "Bolums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Akademisyens_Fakultes_FakulteId",
                table: "Akademisyens",
                column: "FakulteId",
                principalTable: "Fakultes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Akademisyens_Bolums_BolumId",
                table: "Akademisyens");

            migrationBuilder.DropForeignKey(
                name: "FK_Akademisyens_Fakultes_FakulteId",
                table: "Akademisyens");

            migrationBuilder.DropIndex(
                name: "IX_Akademisyens_BolumId",
                table: "Akademisyens");

            migrationBuilder.DropIndex(
                name: "IX_Akademisyens_FakulteId",
                table: "Akademisyens");

            migrationBuilder.DropColumn(
                name: "BolumId",
                table: "Akademisyens");

            migrationBuilder.DropColumn(
                name: "FakulteId",
                table: "Akademisyens");
        }
    }
}
