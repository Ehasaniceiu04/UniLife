using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class donemtipders2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Derss_DonemTips_DonemTipId",
                table: "Derss");

            migrationBuilder.AlterColumn<int>(
                name: "DonemTipId",
                table: "Derss",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_DonemTips_DonemTipId",
                table: "Derss",
                column: "DonemTipId",
                principalTable: "DonemTips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Derss_DonemTips_DonemTipId",
                table: "Derss");

            migrationBuilder.AlterColumn<int>(
                name: "DonemTipId",
                table: "Derss",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

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
