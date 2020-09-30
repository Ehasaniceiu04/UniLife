using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class derskancaAktifkredi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AktifMufredatAkts",
                table: "DersKancas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AktifMufredatDersAd",
                table: "DersKancas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AktifMufredatDersKod",
                table: "DersKancas",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AktifMufredatKredi",
                table: "DersKancas",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AktifMufredatAkts",
                table: "DersKancas");

            migrationBuilder.DropColumn(
                name: "AktifMufredatDersAd",
                table: "DersKancas");

            migrationBuilder.DropColumn(
                name: "AktifMufredatDersKod",
                table: "DersKancas");

            migrationBuilder.DropColumn(
                name: "AktifMufredatKredi",
                table: "DersKancas");
        }
    }
}
