using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Semerkand.Storage.Migrations
{
    public partial class control21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "Programs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DersAcilan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(maxLength: 300, nullable: false),
                    Kod = table.Column<string>(nullable: true),
                    DersId = table.Column<int>(nullable: false),
                    ProgramId = table.Column<int>(nullable: false),
                    DonemTipId = table.Column<int>(nullable: false),
                    KisaAd = table.Column<string>(nullable: true),
                    Akts = table.Column<int>(nullable: false),
                    GecmeNotu = table.Column<int>(nullable: false),
                    OptikKod = table.Column<string>(nullable: true),
                    AdEn = table.Column<string>(nullable: true),
                    UygSaat = table.Column<int>(nullable: false),
                    LabSaat = table.Column<int>(nullable: false),
                    TeoSaat = table.Column<int>(nullable: false),
                    Kredi = table.Column<double>(nullable: false),
                    Durum = table.Column<bool>(nullable: false),
                    Zorunlu = table.Column<int>(nullable: false),
                    Sinif = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersAcilan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DersAcilan_Derss_DersId",
                        column: x => x.DersId,
                        principalTable: "Derss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DersAcilan_DonemTips_DonemTipId",
                        column: x => x.DonemTipId,
                        principalTable: "DonemTips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DersAcilan_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Programs_ProgramId",
                table: "Programs",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilan_DersId",
                table: "DersAcilan",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilan_DonemTipId",
                table: "DersAcilan",
                column: "DonemTipId");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilan_ProgramId",
                table: "DersAcilan",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Programs_ProgramId",
                table: "Programs",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Programs_ProgramId",
                table: "Programs");

            migrationBuilder.DropTable(
                name: "DersAcilan");

            migrationBuilder.DropIndex(
                name: "IX_Programs_ProgramId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Programs");
        }
    }
}
