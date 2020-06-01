using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class control53 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MufredatId",
                table: "DersAcilans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilans_MufredatId",
                table: "DersAcilans",
                column: "MufredatId");

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilans_Mufredats_MufredatId",
                table: "DersAcilans",
                column: "MufredatId",
                principalTable: "Mufredats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilans_Mufredats_MufredatId",
                table: "DersAcilans");

            migrationBuilder.DropIndex(
                name: "IX_DersAcilans_MufredatId",
                table: "DersAcilans");

            migrationBuilder.DropColumn(
                name: "MufredatId",
                table: "DersAcilans");
        }
    }
}
