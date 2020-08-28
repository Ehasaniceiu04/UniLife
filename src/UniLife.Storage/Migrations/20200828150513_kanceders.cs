using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class kanceders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KancaDersId",
                table: "Derss",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KancaDersId",
                table: "Derss");
        }
    }
}
