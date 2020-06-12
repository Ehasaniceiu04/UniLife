using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class control : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilans_Ogretmens_OgretmenId",
                table: "DersAcilans");

            migrationBuilder.AlterColumn<int>(
                name: "OgretmenId",
                table: "DersAcilans",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilans_Ogretmens_OgretmenId",
                table: "DersAcilans",
                column: "OgretmenId",
                principalTable: "Ogretmens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilans_Ogretmens_OgretmenId",
                table: "DersAcilans");

            migrationBuilder.AlterColumn<int>(
                name: "OgretmenId",
                table: "DersAcilans",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilans_Ogretmens_OgretmenId",
                table: "DersAcilans",
                column: "OgretmenId",
                principalTable: "Ogretmens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
