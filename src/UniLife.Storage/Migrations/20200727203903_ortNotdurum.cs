using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class ortNotdurum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Not",
                table: "DersKayits",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Ort",
                table: "DersKayits",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "SonucDurum",
                table: "DersKayits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Not",
                table: "DersKayits");

            migrationBuilder.DropColumn(
                name: "Ort",
                table: "DersKayits");

            migrationBuilder.DropColumn(
                name: "SonucDurum",
                table: "DersKayits");
        }
    }
}
