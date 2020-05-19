using Microsoft.EntityFrameworkCore.Migrations;

namespace Semerkand.Storage.Migrations
{
    public partial class control24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilan_Derss_DersId",
                table: "DersAcilan");

            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilan_DonemTips_DonemTipId",
                table: "DersAcilan");

            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilan_Programs_ProgramId",
                table: "DersAcilan");

            migrationBuilder.DropForeignKey(
                name: "FK_Derss_DonemTips_DonemTipId",
                table: "Derss");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DersAcilan",
                table: "DersAcilan");

            migrationBuilder.RenameTable(
                name: "DersAcilan",
                newName: "DersAcilans");

            migrationBuilder.RenameIndex(
                name: "IX_DersAcilan_ProgramId",
                table: "DersAcilans",
                newName: "IX_DersAcilans_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_DersAcilan_DonemTipId",
                table: "DersAcilans",
                newName: "IX_DersAcilans_DonemTipId");

            migrationBuilder.RenameIndex(
                name: "IX_DersAcilan_DersId",
                table: "DersAcilans",
                newName: "IX_DersAcilans_DersId");

            migrationBuilder.AlterColumn<int>(
                name: "DonemTipId",
                table: "Derss",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "DonemId",
                table: "Derss",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DonemTipId",
                table: "DersAcilans",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "DonemId",
                table: "DersAcilans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DersAcilans",
                table: "DersAcilans",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Derss_DonemId",
                table: "Derss",
                column: "DonemId");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilans_DonemId",
                table: "DersAcilans",
                column: "DonemId");

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilans_Derss_DersId",
                table: "DersAcilans",
                column: "DersId",
                principalTable: "Derss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilans_Donems_DonemId",
                table: "DersAcilans",
                column: "DonemId",
                principalTable: "Donems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilans_DonemTips_DonemTipId",
                table: "DersAcilans",
                column: "DonemTipId",
                principalTable: "DonemTips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilans_Programs_ProgramId",
                table: "DersAcilans",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_Donems_DonemId",
                table: "Derss",
                column: "DonemId",
                principalTable: "Donems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_DonemTips_DonemTipId",
                table: "Derss",
                column: "DonemTipId",
                principalTable: "DonemTips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilans_Derss_DersId",
                table: "DersAcilans");

            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilans_Donems_DonemId",
                table: "DersAcilans");

            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilans_DonemTips_DonemTipId",
                table: "DersAcilans");

            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilans_Programs_ProgramId",
                table: "DersAcilans");

            migrationBuilder.DropForeignKey(
                name: "FK_Derss_Donems_DonemId",
                table: "Derss");

            migrationBuilder.DropForeignKey(
                name: "FK_Derss_DonemTips_DonemTipId",
                table: "Derss");

            migrationBuilder.DropIndex(
                name: "IX_Derss_DonemId",
                table: "Derss");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DersAcilans",
                table: "DersAcilans");

            migrationBuilder.DropIndex(
                name: "IX_DersAcilans_DonemId",
                table: "DersAcilans");

            migrationBuilder.DropColumn(
                name: "DonemId",
                table: "Derss");

            migrationBuilder.DropColumn(
                name: "DonemId",
                table: "DersAcilans");

            migrationBuilder.RenameTable(
                name: "DersAcilans",
                newName: "DersAcilan");

            migrationBuilder.RenameIndex(
                name: "IX_DersAcilans_ProgramId",
                table: "DersAcilan",
                newName: "IX_DersAcilan_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_DersAcilans_DonemTipId",
                table: "DersAcilan",
                newName: "IX_DersAcilan_DonemTipId");

            migrationBuilder.RenameIndex(
                name: "IX_DersAcilans_DersId",
                table: "DersAcilan",
                newName: "IX_DersAcilan_DersId");

            migrationBuilder.AlterColumn<int>(
                name: "DonemTipId",
                table: "Derss",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DonemTipId",
                table: "DersAcilan",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DersAcilan",
                table: "DersAcilan",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilan_Derss_DersId",
                table: "DersAcilan",
                column: "DersId",
                principalTable: "Derss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilan_DonemTips_DonemTipId",
                table: "DersAcilan",
                column: "DonemTipId",
                principalTable: "DonemTips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilan_Programs_ProgramId",
                table: "DersAcilan",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_DonemTips_DonemTipId",
                table: "Derss",
                column: "DonemTipId",
                principalTable: "DonemTips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
