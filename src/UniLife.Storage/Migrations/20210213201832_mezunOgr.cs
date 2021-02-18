using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class mezunOgr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AgNo1",
                table: "Ogrencis",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AgNo2",
                table: "Ogrencis",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "BasarisizDersler",
                table: "Ogrencis",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HasHazirlik",
                table: "Ogrencis",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HasStaj",
                table: "Ogrencis",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SDersler1",
                table: "Ogrencis",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SDersler2",
                table: "Ogrencis",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SonDonem",
                table: "Ogrencis",
                type: "character varying(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TopAkts1",
                table: "Ogrencis",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TopAkts2",
                table: "Ogrencis",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TopKredi1",
                table: "Ogrencis",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TopKredi2",
                table: "Ogrencis",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZDersler1",
                table: "Ogrencis",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZDersler2",
                table: "Ogrencis",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgNo1",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "AgNo2",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "BasarisizDersler",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "HasHazirlik",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "HasStaj",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "SDersler1",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "SDersler2",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "SonDonem",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "TopAkts1",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "TopAkts2",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "TopKredi1",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "TopKredi2",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "ZDersler1",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "ZDersler2",
                table: "Ogrencis");
        }
    }
}
