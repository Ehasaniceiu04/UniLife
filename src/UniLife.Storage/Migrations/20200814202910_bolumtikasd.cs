using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class bolumtikasd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Durum",
                table: "Programs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "KisaAdEn",
                table: "Programs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MezuniyetUnvan",
                table: "Programs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Yerleske",
                table: "Programs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Yillik",
                table: "Programs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "YoksisBirimKod",
                table: "Programs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "YoksisDurum",
                table: "Programs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Durum",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "KisaAdEn",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "MezuniyetUnvan",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Yerleske",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Yillik",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "YoksisBirimKod",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "YoksisDurum",
                table: "Programs");
        }
    }
}
