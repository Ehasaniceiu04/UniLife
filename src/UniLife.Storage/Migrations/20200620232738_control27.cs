using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class control27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OgrId",
                table: "SinavKayits");

            migrationBuilder.AddColumn<int>(
                name: "OgrenciId",
                table: "SinavKayits",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SinavKayits_OgrenciId",
                table: "SinavKayits",
                column: "OgrenciId");

            migrationBuilder.CreateIndex(
                name: "IX_SinavKayits_SinavId",
                table: "SinavKayits",
                column: "SinavId");

            migrationBuilder.AddForeignKey(
                name: "FK_SinavKayits_Ogrencis_OgrenciId",
                table: "SinavKayits",
                column: "OgrenciId",
                principalTable: "Ogrencis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SinavKayits_Sinavs_SinavId",
                table: "SinavKayits",
                column: "SinavId",
                principalTable: "Sinavs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SinavKayits_Ogrencis_OgrenciId",
                table: "SinavKayits");

            migrationBuilder.DropForeignKey(
                name: "FK_SinavKayits_Sinavs_SinavId",
                table: "SinavKayits");

            migrationBuilder.DropIndex(
                name: "IX_SinavKayits_OgrenciId",
                table: "SinavKayits");

            migrationBuilder.DropIndex(
                name: "IX_SinavKayits_SinavId",
                table: "SinavKayits");

            migrationBuilder.DropColumn(
                name: "OgrenciId",
                table: "SinavKayits");

            migrationBuilder.AddColumn<int>(
                name: "OgrId",
                table: "SinavKayits",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
