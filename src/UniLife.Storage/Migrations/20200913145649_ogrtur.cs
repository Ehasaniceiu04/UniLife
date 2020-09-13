using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class ogrtur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ogrencis_OgrenimTurs_OgrenimTurId",
                table: "Ogrencis");

            migrationBuilder.DropIndex(
                name: "IX_Ogrencis_OgrenimTurId",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "OgrenimTurId",
                table: "Ogrencis");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OgrenimTurId",
                table: "Ogrencis",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencis_OgrenimTurId",
                table: "Ogrencis",
                column: "OgrenimTurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrencis_OgrenimTurs_OgrenimTurId",
                table: "Ogrencis",
                column: "OgrenimTurId",
                principalTable: "OgrenimTurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
