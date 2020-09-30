using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class derskancaYerinekredi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PasifMufredatAkts",
                table: "DersKancas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PasifMufredatDersAd",
                table: "DersKancas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasifMufredatDersKod",
                table: "DersKancas",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PasifMufredatKredi",
                table: "DersKancas",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasifMufredatAkts",
                table: "DersKancas");

            migrationBuilder.DropColumn(
                name: "PasifMufredatDersAd",
                table: "DersKancas");

            migrationBuilder.DropColumn(
                name: "PasifMufredatDersKod",
                table: "DersKancas");

            migrationBuilder.DropColumn(
                name: "PasifMufredatKredi",
                table: "DersKancas");
        }
    }
}
