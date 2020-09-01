using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UniLife.Storage.Migrations
{
    public partial class DersKanca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DersKancas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AktifMufredatDersId = table.Column<int>(nullable: false),
                    PasifMufredatDersId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersKancas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DersKancas_Derss_AktifMufredatDersId",
                        column: x => x.AktifMufredatDersId,
                        principalTable: "Derss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DersKancas_Derss_PasifMufredatDersId",
                        column: x => x.PasifMufredatDersId,
                        principalTable: "Derss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DersKancas_AktifMufredatDersId",
                table: "DersKancas",
                column: "AktifMufredatDersId");

            migrationBuilder.CreateIndex(
                name: "IX_DersKancas_PasifMufredatDersId",
                table: "DersKancas",
                column: "PasifMufredatDersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DersKancas");
        }
    }
}
