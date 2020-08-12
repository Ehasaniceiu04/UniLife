using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class akademiktakvim2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkademikTakvims_Fakultes_FakulteId",
                table: "AkademikTakvims");

            migrationBuilder.AlterColumn<int>(
                name: "FakulteId",
                table: "AkademikTakvims",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BitTarih",
                table: "AkademikTakvims",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BasTarih",
                table: "AkademikTakvims",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_AkademikTakvims_Fakultes_FakulteId",
                table: "AkademikTakvims",
                column: "FakulteId",
                principalTable: "Fakultes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkademikTakvims_Fakultes_FakulteId",
                table: "AkademikTakvims");

            migrationBuilder.AlterColumn<int>(
                name: "FakulteId",
                table: "AkademikTakvims",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BitTarih",
                table: "AkademikTakvims",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BasTarih",
                table: "AkademikTakvims",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AkademikTakvims_Fakultes_FakulteId",
                table: "AkademikTakvims",
                column: "FakulteId",
                principalTable: "Fakultes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
