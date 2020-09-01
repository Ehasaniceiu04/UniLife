using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class kriterr2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SinavKriters_ProgramId",
                table: "SinavKriters",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_SinavKriters_Programs_ProgramId",
                table: "SinavKriters",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SinavKriters_Programs_ProgramId",
                table: "SinavKriters");

            migrationBuilder.DropIndex(
                name: "IX_SinavKriters_ProgramId",
                table: "SinavKriters");
        }
    }
}
