using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class control50 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "GenelBakiye",
                table: "Ogrencis",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GerekenTopUcret",
                table: "Ogrencis",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OdenenTopUcret",
                table: "Ogrencis",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenelBakiye",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "GerekenTopUcret",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "OdenenTopUcret",
                table: "Ogrencis");
        }
    }
}
