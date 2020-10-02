using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class kampusbag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KampusId",
                table: "Programs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KampusId",
                table: "Binas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Programs_KampusId",
                table: "Programs",
                column: "KampusId");

            migrationBuilder.CreateIndex(
                name: "IX_Binas_KampusId",
                table: "Binas",
                column: "KampusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Binas_Kampuss_KampusId",
                table: "Binas",
                column: "KampusId",
                principalTable: "Kampuss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Kampuss_KampusId",
                table: "Programs",
                column: "KampusId",
                principalTable: "Kampuss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Binas_Kampuss_KampusId",
                table: "Binas");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Kampuss_KampusId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Programs_KampusId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Binas_KampusId",
                table: "Binas");

            migrationBuilder.DropColumn(
                name: "KampusId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "KampusId",
                table: "Binas");
        }
    }
}
