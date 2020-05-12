using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class control4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Derss_DonemTip_DonemTipId",
                table: "Derss");

            migrationBuilder.DropForeignKey(
                name: "FK_Donem_DonemTip_DonemTipId",
                table: "Donem");

            migrationBuilder.DropForeignKey(
                name: "FK_Harcs_Donem_DonemId",
                table: "Harcs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DonemTip",
                table: "DonemTip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Donem",
                table: "Donem");

            migrationBuilder.RenameTable(
                name: "DonemTip",
                newName: "DonemTips");

            migrationBuilder.RenameTable(
                name: "Donem",
                newName: "Donems");

            migrationBuilder.RenameIndex(
                name: "IX_Donem_DonemTipId",
                table: "Donems",
                newName: "IX_Donems_DonemTipId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DonemTips",
                table: "DonemTips",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Donems",
                table: "Donems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_DonemTips_DonemTipId",
                table: "Derss",
                column: "DonemTipId",
                principalTable: "DonemTips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donems_DonemTips_DonemTipId",
                table: "Donems",
                column: "DonemTipId",
                principalTable: "DonemTips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Harcs_Donems_DonemId",
                table: "Harcs",
                column: "DonemId",
                principalTable: "Donems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Derss_DonemTips_DonemTipId",
                table: "Derss");

            migrationBuilder.DropForeignKey(
                name: "FK_Donems_DonemTips_DonemTipId",
                table: "Donems");

            migrationBuilder.DropForeignKey(
                name: "FK_Harcs_Donems_DonemId",
                table: "Harcs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DonemTips",
                table: "DonemTips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Donems",
                table: "Donems");

            migrationBuilder.RenameTable(
                name: "DonemTips",
                newName: "DonemTip");

            migrationBuilder.RenameTable(
                name: "Donems",
                newName: "Donem");

            migrationBuilder.RenameIndex(
                name: "IX_Donems_DonemTipId",
                table: "Donem",
                newName: "IX_Donem_DonemTipId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DonemTip",
                table: "DonemTip",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Donem",
                table: "Donem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_DonemTip_DonemTipId",
                table: "Derss",
                column: "DonemTipId",
                principalTable: "DonemTip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donem_DonemTip_DonemTipId",
                table: "Donem",
                column: "DonemTipId",
                principalTable: "DonemTip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Harcs_Donem_DonemId",
                table: "Harcs",
                column: "DonemId",
                principalTable: "Donem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
