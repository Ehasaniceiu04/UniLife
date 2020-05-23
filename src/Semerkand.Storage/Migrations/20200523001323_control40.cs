using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class control40 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ogrenci_AspNetUsers_ApplicationUserId",
                table: "Ogrenci");

            migrationBuilder.DropForeignKey(
                name: "FK_Ogrenci_Bolums_BolumId",
                table: "Ogrenci");

            migrationBuilder.DropForeignKey(
                name: "FK_Ogrenci_Fakultes_FakulteId",
                table: "Ogrenci");

            migrationBuilder.DropForeignKey(
                name: "FK_Ogrenci_KayitNedens_KayitNedenId",
                table: "Ogrenci");

            migrationBuilder.DropForeignKey(
                name: "FK_Ogrenci_OgrenimDurums_OgrenimDurumId",
                table: "Ogrenci");

            migrationBuilder.DropForeignKey(
                name: "FK_Ogrenci_Programs_ProgramId",
                table: "Ogrenci");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ogrenci",
                table: "Ogrenci");

            migrationBuilder.RenameTable(
                name: "Ogrenci",
                newName: "Ogrencis");

            migrationBuilder.RenameIndex(
                name: "IX_Ogrenci_ProgramId",
                table: "Ogrencis",
                newName: "IX_Ogrencis_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Ogrenci_OgrenimDurumId",
                table: "Ogrencis",
                newName: "IX_Ogrencis_OgrenimDurumId");

            migrationBuilder.RenameIndex(
                name: "IX_Ogrenci_KayitNedenId",
                table: "Ogrencis",
                newName: "IX_Ogrencis_KayitNedenId");

            migrationBuilder.RenameIndex(
                name: "IX_Ogrenci_FakulteId",
                table: "Ogrencis",
                newName: "IX_Ogrencis_FakulteId");

            migrationBuilder.RenameIndex(
                name: "IX_Ogrenci_BolumId",
                table: "Ogrencis",
                newName: "IX_Ogrencis_BolumId");

            migrationBuilder.RenameIndex(
                name: "IX_Ogrenci_ApplicationUserId",
                table: "Ogrencis",
                newName: "IX_Ogrencis_ApplicationUserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "KayitTarih",
                table: "Ogrencis",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "Eimg",
                table: "Ogrencis",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MufredatId",
                table: "Ogrencis",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sinif",
                table: "Ogrencis",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Soyad",
                table: "Ogrencis",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ogrencis",
                table: "Ogrencis",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencis_MufredatId",
                table: "Ogrencis",
                column: "MufredatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrencis_AspNetUsers_ApplicationUserId",
                table: "Ogrencis",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrencis_Bolums_BolumId",
                table: "Ogrencis",
                column: "BolumId",
                principalTable: "Bolums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrencis_Fakultes_FakulteId",
                table: "Ogrencis",
                column: "FakulteId",
                principalTable: "Fakultes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrencis_KayitNedens_KayitNedenId",
                table: "Ogrencis",
                column: "KayitNedenId",
                principalTable: "KayitNedens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrencis_Mufredats_MufredatId",
                table: "Ogrencis",
                column: "MufredatId",
                principalTable: "Mufredats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrencis_OgrenimDurums_OgrenimDurumId",
                table: "Ogrencis",
                column: "OgrenimDurumId",
                principalTable: "OgrenimDurums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrencis_Programs_ProgramId",
                table: "Ogrencis",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ogrencis_AspNetUsers_ApplicationUserId",
                table: "Ogrencis");

            migrationBuilder.DropForeignKey(
                name: "FK_Ogrencis_Bolums_BolumId",
                table: "Ogrencis");

            migrationBuilder.DropForeignKey(
                name: "FK_Ogrencis_Fakultes_FakulteId",
                table: "Ogrencis");

            migrationBuilder.DropForeignKey(
                name: "FK_Ogrencis_KayitNedens_KayitNedenId",
                table: "Ogrencis");

            migrationBuilder.DropForeignKey(
                name: "FK_Ogrencis_Mufredats_MufredatId",
                table: "Ogrencis");

            migrationBuilder.DropForeignKey(
                name: "FK_Ogrencis_OgrenimDurums_OgrenimDurumId",
                table: "Ogrencis");

            migrationBuilder.DropForeignKey(
                name: "FK_Ogrencis_Programs_ProgramId",
                table: "Ogrencis");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ogrencis",
                table: "Ogrencis");

            migrationBuilder.DropIndex(
                name: "IX_Ogrencis_MufredatId",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "Eimg",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "MufredatId",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "Sinif",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "Soyad",
                table: "Ogrencis");

            migrationBuilder.RenameTable(
                name: "Ogrencis",
                newName: "Ogrenci");

            migrationBuilder.RenameIndex(
                name: "IX_Ogrencis_ProgramId",
                table: "Ogrenci",
                newName: "IX_Ogrenci_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Ogrencis_OgrenimDurumId",
                table: "Ogrenci",
                newName: "IX_Ogrenci_OgrenimDurumId");

            migrationBuilder.RenameIndex(
                name: "IX_Ogrencis_KayitNedenId",
                table: "Ogrenci",
                newName: "IX_Ogrenci_KayitNedenId");

            migrationBuilder.RenameIndex(
                name: "IX_Ogrencis_FakulteId",
                table: "Ogrenci",
                newName: "IX_Ogrenci_FakulteId");

            migrationBuilder.RenameIndex(
                name: "IX_Ogrencis_BolumId",
                table: "Ogrenci",
                newName: "IX_Ogrenci_BolumId");

            migrationBuilder.RenameIndex(
                name: "IX_Ogrencis_ApplicationUserId",
                table: "Ogrenci",
                newName: "IX_Ogrenci_ApplicationUserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "KayitTarih",
                table: "Ogrenci",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ogrenci",
                table: "Ogrenci",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrenci_AspNetUsers_ApplicationUserId",
                table: "Ogrenci",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrenci_Bolums_BolumId",
                table: "Ogrenci",
                column: "BolumId",
                principalTable: "Bolums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrenci_Fakultes_FakulteId",
                table: "Ogrenci",
                column: "FakulteId",
                principalTable: "Fakultes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrenci_KayitNedens_KayitNedenId",
                table: "Ogrenci",
                column: "KayitNedenId",
                principalTable: "KayitNedens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrenci_OgrenimDurums_OgrenimDurumId",
                table: "Ogrenci",
                column: "OgrenimDurumId",
                principalTable: "OgrenimDurums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrenci_Programs_ProgramId",
                table: "Ogrenci",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
