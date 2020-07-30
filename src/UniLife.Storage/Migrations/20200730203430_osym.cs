using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UniLife.Storage.Migrations
{
    public partial class osym : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Osyms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    OsymTip = table.Column<string>(nullable: true),
                    Yil = table.Column<string>(nullable: true),
                    Puan = table.Column<string>(nullable: true),
                    PuanTuru = table.Column<string>(nullable: true),
                    TercihSirasi = table.Column<int>(nullable: false),
                    BolumKodu = table.Column<string>(nullable: true),
                    PuanYuzde = table.Column<string>(nullable: true),
                    BasariSira = table.Column<int>(nullable: false),
                    BasariPuan = table.Column<double>(nullable: false),
                    OkulBirinci = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osyms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Osyms_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Osyms_ApplicationUserId",
                table: "Osyms",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Osyms");
        }
    }
}
