using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class donemtipders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DonemTipId",
                table: "Derss",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Derss_DonemTipId",
                table: "Derss",
                column: "DonemTipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_DonemTips_DonemTipId",
                table: "Derss",
                column: "DonemTipId",
                principalTable: "DonemTips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Derss_DonemTips_DonemTipId",
                table: "Derss");

            migrationBuilder.DropIndex(
                name: "IX_Derss_DonemTipId",
                table: "Derss");

            migrationBuilder.DropColumn(
                name: "DonemTipId",
                table: "Derss");
        }
    }
}
