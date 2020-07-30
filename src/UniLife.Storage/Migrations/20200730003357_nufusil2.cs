using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UniLife.Storage.Migrations
{
    public partial class nufusil2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ils",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(nullable: true),
                    Kod = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ils", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nufuss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    Adres = table.Column<string>(nullable: true),
                    IlId = table.Column<int>(nullable: false),
                    Ilce = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    Banka = table.Column<string>(nullable: true),
                    Sube = table.Column<string>(nullable: true),
                    SubeKod = table.Column<string>(nullable: true),
                    HesapNo = table.Column<string>(nullable: true),
                    Iban = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nufuss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nufuss_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nufuss_Ils_IlId",
                        column: x => x.IlId,
                        principalTable: "Ils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nufuss_ApplicationUserId",
                table: "Nufuss",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Nufuss_IlId",
                table: "Nufuss",
                column: "IlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nufuss");

            migrationBuilder.DropTable(
                name: "Ils");
        }
    }
}
