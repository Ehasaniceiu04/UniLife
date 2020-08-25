using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class programturs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_ProgramTur_ProgramTurId",
                table: "Programs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramTur",
                table: "ProgramTur");

            migrationBuilder.RenameTable(
                name: "ProgramTur",
                newName: "ProgramTurs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramTurs",
                table: "ProgramTurs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_ProgramTurs_ProgramTurId",
                table: "Programs",
                column: "ProgramTurId",
                principalTable: "ProgramTurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_ProgramTurs_ProgramTurId",
                table: "Programs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramTurs",
                table: "ProgramTurs");

            migrationBuilder.RenameTable(
                name: "ProgramTurs",
                newName: "ProgramTur");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramTur",
                table: "ProgramTur",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_ProgramTur_ProgramTurId",
                table: "Programs",
                column: "ProgramTurId",
                principalTable: "ProgramTur",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
