using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class tez : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TezKonu",
                table: "OgrTezs",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Basarili",
                table: "OgrTezs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DanismanId",
                table: "OgrTezs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TezBilgi",
                table: "OgrTezs",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OgrTezs_DanismanId",
                table: "OgrTezs",
                column: "DanismanId");

            migrationBuilder.AddForeignKey(
                name: "FK_OgrTezs_Akademisyens_DanismanId",
                table: "OgrTezs",
                column: "DanismanId",
                principalTable: "Akademisyens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OgrTezs_Akademisyens_DanismanId",
                table: "OgrTezs");

            migrationBuilder.DropIndex(
                name: "IX_OgrTezs_DanismanId",
                table: "OgrTezs");

            migrationBuilder.DropColumn(
                name: "Basarili",
                table: "OgrTezs");

            migrationBuilder.DropColumn(
                name: "DanismanId",
                table: "OgrTezs");

            migrationBuilder.DropColumn(
                name: "TezBilgi",
                table: "OgrTezs");

            migrationBuilder.AlterColumn<string>(
                name: "TezKonu",
                table: "OgrTezs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);
        }
    }
}
