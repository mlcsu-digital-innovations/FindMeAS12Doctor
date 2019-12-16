using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class RefactorContactDetailsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactDetails_Ccgs_CcgId",
                table: "ContactDetails");

            migrationBuilder.DropIndex(
                name: "IX_ContactDetails_CcgId",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "CcgId",
                table: "ContactDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CcgId",
                table: "ContactDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_CcgId",
                table: "ContactDetails",
                column: "CcgId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDetails_Ccgs_CcgId",
                table: "ContactDetails",
                column: "CcgId",
                principalTable: "Ccgs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
