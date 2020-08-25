using Microsoft.EntityFrameworkCore.Migrations;

namespace UniLife.Storage.Migrations
{
    public partial class dil_nedne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DersDilId",
                table: "Derss",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DersNedenId",
                table: "Derss",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DersDilId",
                table: "DersAcilans",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DersNedenId",
                table: "DersAcilans",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Derss_DersDilId",
                table: "Derss",
                column: "DersDilId");

            migrationBuilder.CreateIndex(
                name: "IX_Derss_DersNedenId",
                table: "Derss",
                column: "DersNedenId");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilans_DersDilId",
                table: "DersAcilans",
                column: "DersDilId");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilans_DersNedenId",
                table: "DersAcilans",
                column: "DersNedenId");

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilans_DersDils_DersDilId",
                table: "DersAcilans",
                column: "DersDilId",
                principalTable: "DersDils",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DersAcilans_DersNedens_DersNedenId",
                table: "DersAcilans",
                column: "DersNedenId",
                principalTable: "DersNedens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_DersDils_DersDilId",
                table: "Derss",
                column: "DersDilId",
                principalTable: "DersDils",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Derss_DersNedens_DersNedenId",
                table: "Derss",
                column: "DersNedenId",
                principalTable: "DersNedens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilans_DersDils_DersDilId",
                table: "DersAcilans");

            migrationBuilder.DropForeignKey(
                name: "FK_DersAcilans_DersNedens_DersNedenId",
                table: "DersAcilans");

            migrationBuilder.DropForeignKey(
                name: "FK_Derss_DersDils_DersDilId",
                table: "Derss");

            migrationBuilder.DropForeignKey(
                name: "FK_Derss_DersNedens_DersNedenId",
                table: "Derss");

            migrationBuilder.DropIndex(
                name: "IX_Derss_DersDilId",
                table: "Derss");

            migrationBuilder.DropIndex(
                name: "IX_Derss_DersNedenId",
                table: "Derss");

            migrationBuilder.DropIndex(
                name: "IX_DersAcilans_DersDilId",
                table: "DersAcilans");

            migrationBuilder.DropIndex(
                name: "IX_DersAcilans_DersNedenId",
                table: "DersAcilans");

            migrationBuilder.DropColumn(
                name: "DersDilId",
                table: "Derss");

            migrationBuilder.DropColumn(
                name: "DersNedenId",
                table: "Derss");

            migrationBuilder.DropColumn(
                name: "DersDilId",
                table: "DersAcilans");

            migrationBuilder.DropColumn(
                name: "DersNedenId",
                table: "DersAcilans");
        }
    }
}
