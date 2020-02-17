using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class UserAssessmentClaimsEndPostcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EndPostcode",
                table: "UserAssessmentClaimsAudit",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NextAssessmentId",
                table: "UserAssessmentClaimsAudit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreviousAssessmentId",
                table: "UserAssessmentClaimsAudit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndPostcode",
                table: "UserAssessmentClaims",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NextAssessmentId",
                table: "UserAssessmentClaims",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreviousAssessmentId",
                table: "UserAssessmentClaims",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndPostcode",
                table: "UserAssessmentClaimsAudit");

            migrationBuilder.DropColumn(
                name: "NextAssessmentId",
                table: "UserAssessmentClaimsAudit");

            migrationBuilder.DropColumn(
                name: "PreviousAssessmentId",
                table: "UserAssessmentClaimsAudit");

            migrationBuilder.DropColumn(
                name: "EndPostcode",
                table: "UserAssessmentClaims");

            migrationBuilder.DropColumn(
                name: "NextAssessmentId",
                table: "UserAssessmentClaims");

            migrationBuilder.DropColumn(
                name: "PreviousAssessmentId",
                table: "UserAssessmentClaims");
        }
    }
}
