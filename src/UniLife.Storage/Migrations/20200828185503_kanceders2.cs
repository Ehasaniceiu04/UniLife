using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class kanceders2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KancaDersId",
                table: "Derss");

            migrationBuilder.DropColumn(
                name: "EskiMufBagliDersKod",
                table: "DersAcilans");

            migrationBuilder.AddColumn<string>(
                name: "EskiMufBagliDersId",
                table: "Derss",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EskiMufBagliDersId",
                table: "DersAcilans",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EskiMufBagliDersId",
                table: "Derss");

            migrationBuilder.DropColumn(
                name: "EskiMufBagliDersId",
                table: "DersAcilans");

            migrationBuilder.AddColumn<string>(
                name: "KancaDersId",
                table: "Derss",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EskiMufBagliDersKod",
                table: "DersAcilans",
                type: "text",
                nullable: true);
        }
    }
}
