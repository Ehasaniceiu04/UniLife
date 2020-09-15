using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class geçmenot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GecmeNotu",
                table: "Derss");

            migrationBuilder.DropColumn(
                name: "GecmeNotu",
                table: "DersAcilans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GecmeNotu",
                table: "Derss",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GecmeNotu",
                table: "DersAcilans",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
