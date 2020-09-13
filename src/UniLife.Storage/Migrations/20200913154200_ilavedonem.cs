using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class ilavedonem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ogrencis_Akademisyens_DanismanIkiId",
                table: "Ogrencis");

            migrationBuilder.DropIndex(
                name: "IX_Ogrencis_DanismanIkiId",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "AnaOgrNo",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "DanismanIkiId",
                table: "Ogrencis");

            migrationBuilder.AddColumn<string>(
                name: "IlaveDonem",
                table: "Ogrencis",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IlaveDonem",
                table: "Ogrencis");

            migrationBuilder.AddColumn<string>(
                name: "AnaOgrNo",
                table: "Ogrencis",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DanismanIkiId",
                table: "Ogrencis",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencis_DanismanIkiId",
                table: "Ogrencis",
                column: "DanismanIkiId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrencis_Akademisyens_DanismanIkiId",
                table: "Ogrencis",
                column: "DanismanIkiId",
                principalTable: "Akademisyens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
