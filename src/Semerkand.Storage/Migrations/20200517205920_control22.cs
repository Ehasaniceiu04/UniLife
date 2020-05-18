using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class control22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Programs_ProgramId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Programs_ProgramId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Programs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "Programs",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Programs_ProgramId",
                table: "Programs",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Programs_ProgramId",
                table: "Programs",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
