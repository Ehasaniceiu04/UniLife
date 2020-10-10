using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class babanufus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BabaAd",
                table: "Ogrencis");

            migrationBuilder.AddColumn<string>(
                name: "BabaAd",
                table: "Nufuss",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DogumTarih",
                table: "Nufuss",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DogumYer",
                table: "Nufuss",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BabaAd",
                table: "Nufuss");

            migrationBuilder.DropColumn(
                name: "DogumTarih",
                table: "Nufuss");

            migrationBuilder.DropColumn(
                name: "DogumYer",
                table: "Nufuss");

            migrationBuilder.AddColumn<string>(
                name: "BabaAd",
                table: "Ogrencis",
                type: "text",
                nullable: true);
        }
    }
}
