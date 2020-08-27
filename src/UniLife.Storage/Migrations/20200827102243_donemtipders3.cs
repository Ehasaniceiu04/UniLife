using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class donemtipders3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Derss_Donems_DonemId",
                table: "Derss");

            migrationBuilder.DropIndex(
                name: "IX_Derss_DonemId",
                table: "Derss");

            migrationBuilder.DropColumn(
                name: "DonemId",
                table: "Derss");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DonemId",
                table: "Derss",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Derss_DonemId",
                table: "Derss",
                column: "DonemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_Donems_DonemId",
                table: "Derss",
                column: "DonemId",
                principalTable: "Donems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
