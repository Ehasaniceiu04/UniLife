using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class control22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DersAcilanId",
                table: "Sinavs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sinavs_DersAcilanId",
                table: "Sinavs",
                column: "DersAcilanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sinavs_DersAcilans_DersAcilanId",
                table: "Sinavs",
                column: "DersAcilanId",
                principalTable: "DersAcilans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sinavs_DersAcilans_DersAcilanId",
                table: "Sinavs");

            migrationBuilder.DropIndex(
                name: "IX_Sinavs_DersAcilanId",
                table: "Sinavs");

            migrationBuilder.DropColumn(
                name: "DersAcilanId",
                table: "Sinavs");
        }
    }
}
