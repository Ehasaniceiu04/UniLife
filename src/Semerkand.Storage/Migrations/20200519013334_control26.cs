using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class control26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilans_DonemTips_DonemTipId",
                table: "DersAcilans");

            migrationBuilder.DropForeignKey(
                name: "FK_Derss_DonemTips_DonemTipId",
                table: "Derss");

            migrationBuilder.DropIndex(
                name: "IX_Derss_DonemTipId",
                table: "Derss");

            migrationBuilder.DropIndex(
                name: "IX_DersAcilans_DonemTipId",
                table: "DersAcilans");

            migrationBuilder.DropColumn(
                name: "DonemTipId",
                table: "Derss");

            migrationBuilder.DropColumn(
                name: "DonemTipId",
                table: "DersAcilans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DonemTipId",
                table: "Derss",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DonemTipId",
                table: "DersAcilans",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Derss_DonemTipId",
                table: "Derss",
                column: "DonemTipId");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilans_DonemTipId",
                table: "DersAcilans",
                column: "DonemTipId");

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilans_DonemTips_DonemTipId",
                table: "DersAcilans",
                column: "DonemTipId",
                principalTable: "DonemTips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_DonemTips_DonemTipId",
                table: "Derss",
                column: "DonemTipId",
                principalTable: "DonemTips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
