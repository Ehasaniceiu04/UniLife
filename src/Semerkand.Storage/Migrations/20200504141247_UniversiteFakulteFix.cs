using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class UniversiteFakulteFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Universites");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Fakultes");

            migrationBuilder.AddColumn<string>(
                name: "Isim",
                table: "Universites",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Isim",
                table: "Fakultes",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isim",
                table: "Universites");

            migrationBuilder.DropColumn(
                name: "Isim",
                table: "Fakultes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Universites",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Fakultes",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }
    }
}
