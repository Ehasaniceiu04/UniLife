using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class control11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeçmeNotu",
                table: "Derss");

            migrationBuilder.AddColumn<int>(
                name: "GecmeNotu",
                table: "Derss",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GecmeNotu",
                table: "Derss");

            migrationBuilder.AddColumn<int>(
                name: "GeçmeNotu",
                table: "Derss",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
