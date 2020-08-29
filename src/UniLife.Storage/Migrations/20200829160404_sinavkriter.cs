using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UniLife.Storage.Migrations
{
    public partial class sinavkriter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SinavKriters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SinavDegerlendirmeTipi = table.Column<int>(nullable: false),
                    EnAzYYSEtkiOranYuzde = table.Column<int>(nullable: false),
                    BglDegKatmaLimiti = table.Column<int>(nullable: false),
                    YariYilSonSinavLimit = table.Column<int>(nullable: false),
                    BglDegDisSinOrtTavan = table.Column<int>(nullable: false),
                    BglDegEnAzOgrSayisi = table.Column<int>(nullable: false),
                    IkiAralikEnAzOgrSayisi = table.Column<int>(nullable: false),
                    VarSayilanBagHarfTablosu = table.Column<int>(nullable: false),
                    MutlakDegerHarfTablo = table.Column<int>(nullable: false),
                    BasariDegTabanGeçHarf = table.Column<string>(nullable: true),
                    ButGirmeyenSifir = table.Column<bool>(nullable: false),
                    AraSinavSayi = table.Column<int>(nullable: false),
                    EnAzAraSnvEtkiOrnYuzde = table.Column<int>(nullable: false),
                    EnCokYYSEtkiOranYuzde = table.Column<int>(nullable: false),
                    HamBasariNotAltLimit = table.Column<int>(nullable: false),
                    EnFazlaHamBasariNotuOrt = table.Column<int>(nullable: false),
                    VarSayilanBagilHesapTipi = table.Column<int>(nullable: false),
                    VarsayilanMutlakHesapTipi = table.Column<int>(nullable: false),
                    YYSSinavSayisi = table.Column<int>(nullable: false),
                    FinaleGirmeyenKalir = table.Column<bool>(nullable: false),
                    ButuFinalleAyniDegerlendir = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinavKriters", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SinavKriters");
        }
    }
}
