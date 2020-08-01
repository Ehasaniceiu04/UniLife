using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UniLife.Storage.Migrations
{
    public partial class ogrencidiger2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OgrenciDigers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OgrenciId = table.Column<int>(nullable: false),
                    GelUniv = table.Column<string>(nullable: true),
                    GelBolum = table.Column<string>(nullable: true),
                    GelBirim = table.Column<string>(nullable: true),
                    DondTarih = table.Column<DateTime>(nullable: true),
                    IsDond = table.Column<bool>(nullable: false),
                    CezaTarih = table.Column<DateTime>(nullable: true),
                    CezaAd = table.Column<string>(nullable: true),
                    CezaDesc = table.Column<string>(nullable: true),
                    StajTarihBas = table.Column<DateTime>(nullable: true),
                    StajTarihSon = table.Column<DateTime>(nullable: true),
                    StajSirket = table.Column<string>(nullable: true),
                    TezKonu = table.Column<string>(nullable: true),
                    TezTarih = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrenciDigers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OgrenciDigers_Ogrencis_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrencis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciDigers_OgrenciId",
                table: "OgrenciDigers",
                column: "OgrenciId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OgrenciDigers");
        }
    }
}
