using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class control : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolums_OgrenimTur_OgrenimTurId",
                table: "Bolums");

            migrationBuilder.DropForeignKey(
                name: "FK_Fakultes_FakulteTur_FakulteTurId",
                table: "Fakultes");

            migrationBuilder.DropForeignKey(
                name: "FK_Fakultes_OgrenimTur_OgrenimTurId",
                table: "Fakultes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OgrenimTur",
                table: "OgrenimTur");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FakulteTur",
                table: "FakulteTur");

            migrationBuilder.RenameTable(
                name: "OgrenimTur",
                newName: "OgrenimTurs");

            migrationBuilder.RenameTable(
                name: "FakulteTur",
                newName: "FakulteTurs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OgrenimTurs",
                table: "OgrenimTurs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FakulteTurs",
                table: "FakulteTurs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bolums_OgrenimTurs_OgrenimTurId",
                table: "Bolums",
                column: "OgrenimTurId",
                principalTable: "OgrenimTurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fakultes_FakulteTurs_FakulteTurId",
                table: "Fakultes",
                column: "FakulteTurId",
                principalTable: "FakulteTurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fakultes_OgrenimTurs_OgrenimTurId",
                table: "Fakultes",
                column: "OgrenimTurId",
                principalTable: "OgrenimTurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolums_OgrenimTurs_OgrenimTurId",
                table: "Bolums");

            migrationBuilder.DropForeignKey(
                name: "FK_Fakultes_FakulteTurs_FakulteTurId",
                table: "Fakultes");

            migrationBuilder.DropForeignKey(
                name: "FK_Fakultes_OgrenimTurs_OgrenimTurId",
                table: "Fakultes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OgrenimTurs",
                table: "OgrenimTurs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FakulteTurs",
                table: "FakulteTurs");

            migrationBuilder.RenameTable(
                name: "OgrenimTurs",
                newName: "OgrenimTur");

            migrationBuilder.RenameTable(
                name: "FakulteTurs",
                newName: "FakulteTur");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OgrenimTur",
                table: "OgrenimTur",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FakulteTur",
                table: "FakulteTur",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bolums_OgrenimTur_OgrenimTurId",
                table: "Bolums",
                column: "OgrenimTurId",
                principalTable: "OgrenimTur",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fakultes_FakulteTur_FakulteTurId",
                table: "Fakultes",
                column: "FakulteTurId",
                principalTable: "FakulteTur",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fakultes_OgrenimTur_OgrenimTurId",
                table: "Fakultes",
                column: "OgrenimTurId",
                principalTable: "OgrenimTur",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
