using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class control57 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersKayit_DersAcilans_DersAcilanId",
                table: "DersKayit");

            migrationBuilder.DropForeignKey(
                name: "FK_DersKayit_Ogrencis_OgrenciId",
                table: "DersKayit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DersKayit",
                table: "DersKayit");

            migrationBuilder.RenameTable(
                name: "DersKayit",
                newName: "DersKayits");

            migrationBuilder.RenameIndex(
                name: "IX_DersKayit_OgrenciId",
                table: "DersKayits",
                newName: "IX_DersKayits_OgrenciId");

            migrationBuilder.RenameIndex(
                name: "IX_DersKayit_DersAcilanId",
                table: "DersKayits",
                newName: "IX_DersKayits_DersAcilanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DersKayits",
                table: "DersKayits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DersKayits_DersAcilans_DersAcilanId",
                table: "DersKayits",
                column: "DersAcilanId",
                principalTable: "DersAcilans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DersKayits_Ogrencis_OgrenciId",
                table: "DersKayits",
                column: "OgrenciId",
                principalTable: "Ogrencis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersKayits_DersAcilans_DersAcilanId",
                table: "DersKayits");

            migrationBuilder.DropForeignKey(
                name: "FK_DersKayits_Ogrencis_OgrenciId",
                table: "DersKayits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DersKayits",
                table: "DersKayits");

            migrationBuilder.RenameTable(
                name: "DersKayits",
                newName: "DersKayit");

            migrationBuilder.RenameIndex(
                name: "IX_DersKayits_OgrenciId",
                table: "DersKayit",
                newName: "IX_DersKayit_OgrenciId");

            migrationBuilder.RenameIndex(
                name: "IX_DersKayits_DersAcilanId",
                table: "DersKayit",
                newName: "IX_DersKayit_DersAcilanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DersKayit",
                table: "DersKayit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DersKayit_DersAcilans_DersAcilanId",
                table: "DersKayit",
                column: "DersAcilanId",
                principalTable: "DersAcilans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DersKayit_Ogrencis_OgrenciId",
                table: "DersKayit",
                column: "OgrenciId",
                principalTable: "Ogrencis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
