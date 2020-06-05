using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class control65 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DersSecilenAd",
                table: "DersKayits");

            migrationBuilder.DropColumn(
                name: "DersSecilenId",
                table: "DersKayits");

            migrationBuilder.AddColumn<string>(
                name: "DersYerineSecilenAd",
                table: "DersKayits",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DersYerineSecilenId",
                table: "DersKayits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DersYerineSecilenAd",
                table: "DersKayits");

            migrationBuilder.DropColumn(
                name: "DersYerineSecilenId",
                table: "DersKayits");

            migrationBuilder.AddColumn<string>(
                name: "DersSecilenAd",
                table: "DersKayits",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DersSecilenId",
                table: "DersKayits",
                type: "integer",
                nullable: true);
        }
    }
}
