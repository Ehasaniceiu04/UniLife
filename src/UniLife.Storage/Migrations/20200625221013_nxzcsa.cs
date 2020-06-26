using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class nxzcsa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AltKont",
                table: "DersAcilans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AltKota",
                table: "DersAcilans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BolDisKont",
                table: "DersAcilans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BolDisKota",
                table: "DersAcilans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TopKont",
                table: "DersAcilans",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AltKont",
                table: "DersAcilans");

            migrationBuilder.DropColumn(
                name: "AltKota",
                table: "DersAcilans");

            migrationBuilder.DropColumn(
                name: "BolDisKont",
                table: "DersAcilans");

            migrationBuilder.DropColumn(
                name: "BolDisKota",
                table: "DersAcilans");

            migrationBuilder.DropColumn(
                name: "TopKont",
                table: "DersAcilans");
        }
    }
}
