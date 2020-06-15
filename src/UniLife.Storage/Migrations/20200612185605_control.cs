using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class control : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilans_Akademisyens_AkademisyenId",
                table: "DersAcilans");

            migrationBuilder.AlterColumn<int>(
                name: "AkademisyenId",
                table: "DersAcilans",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilans_Akademisyens_AkademisyenId",
                table: "DersAcilans",
                column: "AkademisyenId",
                principalTable: "Akademisyens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilans_Akademisyens_AkademisyenId",
                table: "DersAcilans");

            migrationBuilder.AlterColumn<int>(
                name: "AkademisyenId",
                table: "DersAcilans",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilans_Akademisyens_AkademisyenId",
                table: "DersAcilans",
                column: "AkademisyenId",
                principalTable: "Akademisyens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
