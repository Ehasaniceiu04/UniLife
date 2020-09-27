using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class akano : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OgrtNo",
                table: "Akademisyens");

            migrationBuilder.AlterColumn<long>(
                name: "PersNo",
                table: "Personels",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AkaNo",
                table: "Akademisyens",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AkaNo",
                table: "Akademisyens");

            migrationBuilder.AlterColumn<string>(
                name: "PersNo",
                table: "Personels",
                type: "text",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<string>(
                name: "OgrtNo",
                table: "Akademisyens",
                type: "text",
                nullable: true);
        }
    }
}
