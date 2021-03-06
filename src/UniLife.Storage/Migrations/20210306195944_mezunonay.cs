using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class mezunonay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DanismanOnay",
                table: "Ogrencis");

            migrationBuilder.AddColumn<int>(
                name: "MezunOnay",
                table: "Ogrencis",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MezunOnay",
                table: "Ogrencis");

            migrationBuilder.AddColumn<bool>(
                name: "DanismanOnay",
                table: "Ogrencis",
                type: "boolean",
                nullable: true);
        }
    }
}
