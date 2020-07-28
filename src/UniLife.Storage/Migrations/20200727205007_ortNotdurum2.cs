using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class ortNotdurum2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SonucDurum",
                table: "DersKayits",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sonuc",
                table: "DersKayits",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sonuc",
                table: "DersKayits");

            migrationBuilder.AlterColumn<string>(
                name: "SonucDurum",
                table: "DersKayits",
                type: "text",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
