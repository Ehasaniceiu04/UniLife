using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UniLife.Storage.Migrations
{
    public partial class yabancibasvuru : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YabancıBasvurus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ulke = table.Column<string>(nullable: true),
                    Sehir = table.Column<string>(nullable: true),
                    Ad = table.Column<string>(nullable: true),
                    Soyad = table.Column<string>(nullable: true),
                    BabaAd = table.Column<string>(nullable: true),
                    AnaAd = table.Column<string>(nullable: true),
                    DogumYer = table.Column<string>(nullable: true),
                    DogumTarih = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Cinsiyet = table.Column<string>(nullable: true),
                    PassportNo = table.Column<string>(nullable: true),
                    YasaUlke = table.Column<string>(nullable: true),
                    IletisimBilgisi = table.Column<string>(nullable: true),
                    Adres = table.Column<string>(nullable: true),
                    LiseMezuniyetAlan = table.Column<string>(nullable: true),
                    LiseDurum = table.Column<string>(nullable: true),
                    LiseMezunNot = table.Column<decimal>(nullable: false),
                    Tercih1 = table.Column<string>(nullable: true),
                    Tercih2 = table.Column<string>(nullable: true),
                    Tercih3 = table.Column<string>(nullable: true),
                    Tercih4 = table.Column<string>(nullable: true),
                    Tercih5 = table.Column<string>(nullable: true),
                    Accept = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YabancıBasvurus", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YabancıBasvurus");
        }
    }
}
