using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class control46 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "IsNavMinified",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "IsNavOpen",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "LastPageVisited",
                table: "Ogrencis");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "Ogrencis");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Ogrencis",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsNavMinified",
                table: "Ogrencis",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsNavOpen",
                table: "Ogrencis",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastPageVisited",
                table: "Ogrencis",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "Ogrencis",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
