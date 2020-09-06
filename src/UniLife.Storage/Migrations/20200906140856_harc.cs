using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class harc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "OnTutar",
                table: "Harcs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Tutar",
                table: "Harcs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "YabanciTutar",
                table: "Harcs",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnTutar",
                table: "Harcs");

            migrationBuilder.DropColumn(
                name: "Tutar",
                table: "Harcs");

            migrationBuilder.DropColumn(
                name: "YabanciTutar",
                table: "Harcs");
        }
    }
}
