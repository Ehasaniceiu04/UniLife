using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class dersorgNots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GecDurum",
                table: "DersKayits",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "HBN",
                table: "DersKayits",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HarfNot",
                table: "DersKayits",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TSkor",
                table: "DersKayits",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GecDurum",
                table: "DersKayits");

            migrationBuilder.DropColumn(
                name: "HBN",
                table: "DersKayits");

            migrationBuilder.DropColumn(
                name: "HarfNot",
                table: "DersKayits");

            migrationBuilder.DropColumn(
                name: "TSkor",
                table: "DersKayits");
        }
    }
}
