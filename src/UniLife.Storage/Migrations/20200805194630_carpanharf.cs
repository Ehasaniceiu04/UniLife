using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class carpanharf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Not",
                table: "DersKayits");

            migrationBuilder.AddColumn<double>(
                name: "Carpan",
                table: "DersKayits",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carpan",
                table: "DersKayits");

            migrationBuilder.AddColumn<string>(
                name: "Not",
                table: "DersKayits",
                type: "text",
                nullable: true);
        }
    }
}
