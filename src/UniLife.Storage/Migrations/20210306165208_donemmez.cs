using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class donemmez : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SonDonem",
                table: "Ogrencis");

            migrationBuilder.AddColumn<int>(
                name: "SonDonemId",
                table: "Ogrencis",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SonDonemId",
                table: "Ogrencis");

            migrationBuilder.AddColumn<string>(
                name: "SonDonem",
                table: "Ogrencis",
                type: "character varying(11)",
                maxLength: 11,
                nullable: true);
        }
    }
}
