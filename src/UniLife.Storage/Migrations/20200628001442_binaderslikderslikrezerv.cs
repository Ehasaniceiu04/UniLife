using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UniLife.Storage.Migrations
{
    public partial class binaderslikderslikrezerv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Binas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(maxLength: 300, nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Binas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dersliks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(nullable: true),
                    Capacity = table.Column<int>(nullable: false),
                    Color = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    BinaId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dersliks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dersliks_Binas_BinaId",
                        column: x => x.BinaId,
                        principalTable: "Binas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DerslikRezervs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Subject = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    IsAllDay = table.Column<bool>(nullable: true),
                    CategoryColor = table.Column<string>(nullable: true),
                    RecurrenceRule = table.Column<string>(nullable: true),
                    RecurrenceID = table.Column<int>(nullable: true),
                    RecurrenceException = table.Column<string>(nullable: true),
                    StartTimezone = table.Column<string>(nullable: true),
                    EndTimezone = table.Column<string>(nullable: true),
                    DerslikId = table.Column<int>(nullable: false),
                    IsBlock = table.Column<bool>(nullable: false),
                    ElementType = table.Column<string>(nullable: true),
                    StartTimeValue = table.Column<DateTime>(nullable: false),
                    EndTimeValue = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DerslikRezervs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DerslikRezervs_Dersliks_DerslikId",
                        column: x => x.DerslikId,
                        principalTable: "Dersliks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DerslikRezervs_DerslikId",
                table: "DerslikRezervs",
                column: "DerslikId");

            migrationBuilder.CreateIndex(
                name: "IX_Dersliks_BinaId",
                table: "Dersliks",
                column: "BinaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DerslikRezervs");

            migrationBuilder.DropTable(
                name: "Dersliks");

            migrationBuilder.DropTable(
                name: "Binas");
        }
    }
}
