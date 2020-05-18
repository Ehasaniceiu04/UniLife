using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class control20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Derss");

            migrationBuilder.AddColumn<bool>(
                name: "Aktif",
                table: "Mufredats",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aktif",
                table: "Mufredats");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Derss",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
