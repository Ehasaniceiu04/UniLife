using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class control6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ogrenci_KayitNeden_KayitNedenId",
                table: "Ogrenci");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KayitNeden",
                table: "KayitNeden");

            migrationBuilder.RenameTable(
                name: "KayitNeden",
                newName: "KayitNedens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KayitNedens",
                table: "KayitNedens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrenci_KayitNedens_KayitNedenId",
                table: "Ogrenci",
                column: "KayitNedenId",
                principalTable: "KayitNedens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ogrenci_KayitNedens_KayitNedenId",
                table: "Ogrenci");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KayitNedens",
                table: "KayitNedens");

            migrationBuilder.RenameTable(
                name: "KayitNedens",
                newName: "KayitNeden");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KayitNeden",
                table: "KayitNeden",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrenci_KayitNeden_KayitNedenId",
                table: "Ogrenci",
                column: "KayitNedenId",
                principalTable: "KayitNeden",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
