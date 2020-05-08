using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class fixmOdel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mufredats_Programs_ProgramId",
                table: "Mufredats");

            migrationBuilder.DropIndex(
                name: "IX_Mufredats_ProgramId",
                table: "Mufredats");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Mufredats");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "Mufredats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Mufredats_ProgramId",
                table: "Mufredats",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mufredats_Programs_ProgramId",
                table: "Mufredats",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
