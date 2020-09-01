using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class rezervSinav : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DerslikRezervs_DersAcilans_DersAcilanId",
                table: "DerslikRezervs");

            migrationBuilder.AlterColumn<int>(
                name: "DersAcilanId",
                table: "DerslikRezervs",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "SinavId",
                table: "DerslikRezervs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DerslikRezervs_SinavId",
                table: "DerslikRezervs",
                column: "SinavId");

            migrationBuilder.AddForeignKey(
                name: "FK_DerslikRezervs_DersAcilans_DersAcilanId",
                table: "DerslikRezervs",
                column: "DersAcilanId",
                principalTable: "DersAcilans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DerslikRezervs_Sinavs_SinavId",
                table: "DerslikRezervs",
                column: "SinavId",
                principalTable: "Sinavs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DerslikRezervs_DersAcilans_DersAcilanId",
                table: "DerslikRezervs");

            migrationBuilder.DropForeignKey(
                name: "FK_DerslikRezervs_Sinavs_SinavId",
                table: "DerslikRezervs");

            migrationBuilder.DropIndex(
                name: "IX_DerslikRezervs_SinavId",
                table: "DerslikRezervs");

            migrationBuilder.DropColumn(
                name: "SinavId",
                table: "DerslikRezervs");

            migrationBuilder.AlterColumn<int>(
                name: "DersAcilanId",
                table: "DerslikRezervs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DerslikRezervs_DersAcilans_DersAcilanId",
                table: "DerslikRezervs",
                column: "DersAcilanId",
                principalTable: "DersAcilans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
