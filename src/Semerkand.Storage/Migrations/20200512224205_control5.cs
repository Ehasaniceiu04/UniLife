using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class control5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ogrenci_OgrenimDurum_OgrenimDurumId",
                table: "Ogrenci");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OgrenimDurum",
                table: "OgrenimDurum");

            migrationBuilder.RenameTable(
                name: "OgrenimDurum",
                newName: "OgrenimDurums");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OgrenimDurums",
                table: "OgrenimDurums",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrenci_OgrenimDurums_OgrenimDurumId",
                table: "Ogrenci",
                column: "OgrenimDurumId",
                principalTable: "OgrenimDurums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ogrenci_OgrenimDurums_OgrenimDurumId",
                table: "Ogrenci");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OgrenimDurums",
                table: "OgrenimDurums");

            migrationBuilder.RenameTable(
                name: "OgrenimDurums",
                newName: "OgrenimDurum");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OgrenimDurum",
                table: "OgrenimDurum",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrenci_OgrenimDurum_OgrenimDurumId",
                table: "Ogrenci",
                column: "OgrenimDurumId",
                principalTable: "OgrenimDurum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
