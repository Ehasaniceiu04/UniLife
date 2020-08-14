using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class fakultemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fakultes_FakulteTurs_FakulteTurId",
                table: "Fakultes");

            migrationBuilder.DropIndex(
                name: "IX_Fakultes_FakulteTurId",
                table: "Fakultes");

            migrationBuilder.DropColumn(
                name: "DiplomaAd",
                table: "Fakultes");

            migrationBuilder.DropColumn(
                name: "FakulteTurId",
                table: "Fakultes");

            migrationBuilder.AddColumn<string>(
                name: "AdEnKisa",
                table: "Fakultes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdEnKisa",
                table: "Fakultes");

            migrationBuilder.AddColumn<string>(
                name: "DiplomaAd",
                table: "Fakultes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FakulteTurId",
                table: "Fakultes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fakultes_FakulteTurId",
                table: "Fakultes",
                column: "FakulteTurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fakultes_FakulteTurs_FakulteTurId",
                table: "Fakultes",
                column: "FakulteTurId",
                principalTable: "FakulteTurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
