using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class SsapmaOrt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SinifOrtalamasi",
                table: "DersAcilans",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SinifStandartSapma",
                table: "DersAcilans",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SinifOrtalamasi",
                table: "DersAcilans");

            migrationBuilder.DropColumn(
                name: "SinifStandartSapma",
                table: "DersAcilans");
        }
    }
}
