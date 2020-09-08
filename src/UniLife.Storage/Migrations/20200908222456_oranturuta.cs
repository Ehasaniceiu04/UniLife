using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class oranturuta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Durum",
                table: "OgrBursBasaris",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOran",
                table: "OgrBursBasaris",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Tutar",
                table: "OgrBursBasaris",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Durum",
                table: "OgrBursBasaris");

            migrationBuilder.DropColumn(
                name: "IsOran",
                table: "OgrBursBasaris");

            migrationBuilder.DropColumn(
                name: "Tutar",
                table: "OgrBursBasaris");
        }
    }
}
