using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class ogrenşmdurum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdEn",
                table: "OgrenimDurums",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Aktif",
                table: "OgrenimDurums",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "KisaAd",
                table: "OgrenimDurums",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KisaAdEn",
                table: "OgrenimDurums",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgrenciDurum",
                table: "OgrenimDurums",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YoksisKod",
                table: "OgrenimDurums",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YoksisStatuKod",
                table: "OgrenimDurums",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdEn",
                table: "OgrenimDurums");

            migrationBuilder.DropColumn(
                name: "Aktif",
                table: "OgrenimDurums");

            migrationBuilder.DropColumn(
                name: "KisaAd",
                table: "OgrenimDurums");

            migrationBuilder.DropColumn(
                name: "KisaAdEn",
                table: "OgrenimDurums");

            migrationBuilder.DropColumn(
                name: "OgrenciDurum",
                table: "OgrenimDurums");

            migrationBuilder.DropColumn(
                name: "YoksisKod",
                table: "OgrenimDurums");

            migrationBuilder.DropColumn(
                name: "YoksisStatuKod",
                table: "OgrenimDurums");
        }
    }
}
