using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class zxcasd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHazırlık",
                table: "Programs");

            migrationBuilder.AddColumn<bool>(
                name: "IsHazirlik",
                table: "Programs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Programs_FakulteId",
                table: "Programs",
                column: "FakulteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Fakultes_FakulteId",
                table: "Programs",
                column: "FakulteId",
                principalTable: "Fakultes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Fakultes_FakulteId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Programs_FakulteId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "IsHazirlik",
                table: "Programs");

            migrationBuilder.AddColumn<bool>(
                name: "IsHazırlık",
                table: "Programs",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
