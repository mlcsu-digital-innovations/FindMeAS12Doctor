using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class AddLocationToAssessmentDoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactDetailId",
                table: "AssessmentDoctors",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "AssessmentDoctors",
                type: "decimal(8,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "AssessmentDoctors",
                type: "decimal(9,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "AssessmentDoctors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentDoctors_ContactDetailId",
                table: "AssessmentDoctors",
                column: "ContactDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentDoctors_ContactDetails_ContactDetailId",
                table: "AssessmentDoctors",
                column: "ContactDetailId",
                principalTable: "ContactDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentDoctors_ContactDetails_ContactDetailId",
                table: "AssessmentDoctors");

            migrationBuilder.DropIndex(
                name: "IX_AssessmentDoctors_ContactDetailId",
                table: "AssessmentDoctors");

            migrationBuilder.DropColumn(
                name: "ContactDetailId",
                table: "AssessmentDoctors");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "AssessmentDoctors");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "AssessmentDoctors");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "AssessmentDoctors");
        }
    }
}
