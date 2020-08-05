using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class sınavkayıtmazeret2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MazeretiVar",
                table: "SinavKayits");

            migrationBuilder.AddColumn<long>(
                name: "MazeretiSinavKayitId",
                table: "SinavKayits",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MazeretiSinavKayitId",
                table: "SinavKayits");

            migrationBuilder.AddColumn<bool>(
                name: "MazeretiVar",
                table: "SinavKayits",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
