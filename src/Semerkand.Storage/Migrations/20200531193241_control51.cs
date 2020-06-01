using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class control51 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ADKayit",
                table: "DersAcilans",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ODTekrar",
                table: "DersAcilans",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ADKayit",
                table: "DersAcilans");

            migrationBuilder.DropColumn(
                name: "ODTekrar",
                table: "DersAcilans");
        }
    }
}
