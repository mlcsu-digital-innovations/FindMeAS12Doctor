using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class AddDistanceToAssessmentDoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactDetailId",
                table: "AssessmentDoctorsAudit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Distance",
                table: "AssessmentDoctorsAudit",
                type: "decimal(9,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "AssessmentDoctorsAudit",
                type: "decimal(8,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "AssessmentDoctorsAudit",
                type: "decimal(9,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "AssessmentDoctorsAudit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Distance",
                table: "AssessmentDoctors",
                type: "decimal(9,6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactDetailId",
                table: "AssessmentDoctorsAudit");

            migrationBuilder.DropColumn(
                name: "Distance",
                table: "AssessmentDoctorsAudit");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "AssessmentDoctorsAudit");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "AssessmentDoctorsAudit");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "AssessmentDoctorsAudit");

            migrationBuilder.DropColumn(
                name: "Distance",
                table: "AssessmentDoctors");
        }
    }
}
