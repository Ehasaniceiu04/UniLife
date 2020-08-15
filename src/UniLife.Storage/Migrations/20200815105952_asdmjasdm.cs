using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class asdmjasdm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdEn",
                table: "KayitNedens",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DersKayitKuralDisi",
                table: "KayitNedens",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HarcTahakkukEtmez",
                table: "KayitNedens",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "KisaAd",
                table: "KayitNedens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KisaAdEn",
                table: "KayitNedens",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OzelOgr",
                table: "KayitNedens",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "YoksisKod",
                table: "KayitNedens",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YoksisStatusKod",
                table: "KayitNedens",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "YuzdeOn",
                table: "KayitNedens",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdEn",
                table: "KayitNedens");

            migrationBuilder.DropColumn(
                name: "DersKayitKuralDisi",
                table: "KayitNedens");

            migrationBuilder.DropColumn(
                name: "HarcTahakkukEtmez",
                table: "KayitNedens");

            migrationBuilder.DropColumn(
                name: "KisaAd",
                table: "KayitNedens");

            migrationBuilder.DropColumn(
                name: "KisaAdEn",
                table: "KayitNedens");

            migrationBuilder.DropColumn(
                name: "OzelOgr",
                table: "KayitNedens");

            migrationBuilder.DropColumn(
                name: "YoksisKod",
                table: "KayitNedens");

            migrationBuilder.DropColumn(
                name: "YoksisStatusKod",
                table: "KayitNedens");

            migrationBuilder.DropColumn(
                name: "YuzdeOn",
                table: "KayitNedens");
        }
    }
}
