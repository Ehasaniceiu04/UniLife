using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class dil_nedn2e : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilans_DersNedens_DersNedenId",
                table: "DersAcilans");

            migrationBuilder.DropForeignKey(
                name: "FK_Derss_DersNedens_DersNedenId",
                table: "Derss");

            migrationBuilder.AlterColumn<int>(
                name: "DersNedenId",
                table: "Derss",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DersNedenId",
                table: "DersAcilans",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilans_DersNedens_DersNedenId",
                table: "DersAcilans",
                column: "DersNedenId",
                principalTable: "DersNedens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_DersNedens_DersNedenId",
                table: "Derss",
                column: "DersNedenId",
                principalTable: "DersNedens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilans_DersNedens_DersNedenId",
                table: "DersAcilans");

            migrationBuilder.DropForeignKey(
                name: "FK_Derss_DersNedens_DersNedenId",
                table: "Derss");

            migrationBuilder.AlterColumn<int>(
                name: "DersNedenId",
                table: "Derss",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DersNedenId",
                table: "DersAcilans",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilans_DersNedens_DersNedenId",
                table: "DersAcilans",
                column: "DersNedenId",
                principalTable: "DersNedens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_DersNedens_DersNedenId",
                table: "Derss",
                column: "DersNedenId",
                principalTable: "DersNedens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
