using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class akaEx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Diller",
                table: "Akademisyens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EgitimBilg",
                table: "Akademisyens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "Akademisyens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefon2",
                table: "Akademisyens",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diller",
                table: "Akademisyens");

            migrationBuilder.DropColumn(
                name: "EgitimBilg",
                table: "Akademisyens");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "Akademisyens");

            migrationBuilder.DropColumn(
                name: "Telefon2",
                table: "Akademisyens");
        }
    }
}
