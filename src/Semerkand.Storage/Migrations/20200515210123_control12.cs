using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class control12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Derss_Mufredats_MufredatID",
                table: "Derss");

            migrationBuilder.RenameColumn(
                name: "MufredatID",
                table: "Derss",
                newName: "MufredatId");

            migrationBuilder.RenameIndex(
                name: "IX_Derss_MufredatID",
                table: "Derss",
                newName: "IX_Derss_MufredatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_Mufredats_MufredatId",
                table: "Derss",
                column: "MufredatId",
                principalTable: "Mufredats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Derss_Mufredats_MufredatId",
                table: "Derss");

            migrationBuilder.RenameColumn(
                name: "MufredatId",
                table: "Derss",
                newName: "MufredatID");

            migrationBuilder.RenameIndex(
                name: "IX_Derss_MufredatId",
                table: "Derss",
                newName: "IX_Derss_MufredatID");

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_Mufredats_MufredatID",
                table: "Derss",
                column: "MufredatID",
                principalTable: "Mufredats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
