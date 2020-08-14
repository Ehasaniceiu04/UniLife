using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UniLife.Storage.Migrations
{
    public partial class ogrharc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OgrHarcs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OgrenciId = table.Column<int>(nullable: false),
                    DonemId = table.Column<int>(nullable: false),
                    Tipi = table.Column<string>(nullable: true),
                    Turu = table.Column<string>(nullable: true),
                    Tutar = table.Column<double>(nullable: false),
                    IadeTutar = table.Column<double>(nullable: false),
                    TahakkukTarih = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrHarcs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OgrHarcs_Donems_DonemId",
                        column: x => x.DonemId,
                        principalTable: "Donems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OgrHarcs_Ogrencis_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrencis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OgrHarcs_DonemId",
                table: "OgrHarcs",
                column: "DonemId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrHarcs_OgrenciId",
                table: "OgrHarcs",
                column: "OgrenciId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OgrHarcs");
        }
    }
}
