using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class typsda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EskiMufBagliDersId",
                table: "Derss");

            migrationBuilder.AddColumn<string>(
                name: "KancalananDersAd",
                table: "Derss",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KancalananDersAd",
                table: "Derss");

            migrationBuilder.AddColumn<string>(
                name: "EskiMufBagliDersId",
                table: "Derss",
                type: "text",
                nullable: true);
        }
    }
}
