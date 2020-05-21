using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class control30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TCKN",
                table: "Ogrenci",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TCKN",
                table: "AspNetUsers",
                maxLength: 11,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TCKN",
                table: "Ogrenci");

            migrationBuilder.DropColumn(
                name: "TCKN",
                table: "AspNetUsers");
        }
    }
}
