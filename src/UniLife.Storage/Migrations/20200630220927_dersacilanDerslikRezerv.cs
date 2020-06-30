using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class dersacilanDerslikRezerv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DersAcilanId",
                table: "DerslikRezervs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DerslikRezervs_DersAcilanId",
                table: "DerslikRezervs",
                column: "DersAcilanId");

            migrationBuilder.AddForeignKey(
                name: "FK_DerslikRezervs_DersAcilans_DersAcilanId",
                table: "DerslikRezervs",
                column: "DersAcilanId",
                principalTable: "DersAcilans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DerslikRezervs_DersAcilans_DersAcilanId",
                table: "DerslikRezervs");

            migrationBuilder.DropIndex(
                name: "IX_DerslikRezervs_DersAcilanId",
                table: "DerslikRezervs");

            migrationBuilder.DropColumn(
                name: "DersAcilanId",
                table: "DerslikRezervs");
        }
    }
}
