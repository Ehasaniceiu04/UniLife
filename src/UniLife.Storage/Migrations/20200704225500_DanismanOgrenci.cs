using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class DanismanOgrenci : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DanismanId",
                table: "Ogrencis",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DanismanIkiId",
                table: "Ogrencis",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencis_DanismanId",
                table: "Ogrencis",
                column: "DanismanId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencis_DanismanIkiId",
                table: "Ogrencis",
                column: "DanismanIkiId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrencis_Akademisyens_DanismanId",
                table: "Ogrencis",
                column: "DanismanId",
                principalTable: "Akademisyens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrencis_Akademisyens_DanismanIkiId",
                table: "Ogrencis",
                column: "DanismanIkiId",
                principalTable: "Akademisyens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ogrencis_Akademisyens_DanismanId",
                table: "Ogrencis");

            migrationBuilder.DropForeignKey(
                name: "FK_Ogrencis_Akademisyens_DanismanIkiId",
                table: "Ogrencis");

            migrationBuilder.DropIndex(
                name: "IX_Ogrencis_DanismanId",
                table: "Ogrencis");

            migrationBuilder.DropIndex(
                name: "IX_Ogrencis_DanismanIkiId",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "DanismanId",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "DanismanIkiId",
                table: "Ogrencis");
        }
    }
}
