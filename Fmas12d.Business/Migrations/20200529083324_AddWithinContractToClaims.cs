using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class AddWithinContractToClaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUsersPatient",
                table: "UserAssessmentClaimsAudit");

            migrationBuilder.DropColumn(
                name: "IsUsersPatient",
                table: "UserAssessmentClaims");

            migrationBuilder.AddColumn<bool>(
                name: "IsWithinContract",
                table: "UserAssessmentClaimsAudit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsWithinContract",
                table: "UserAssessmentClaims",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWithinContract",
                table: "UserAssessmentClaimsAudit");

            migrationBuilder.DropColumn(
                name: "IsWithinContract",
                table: "UserAssessmentClaims");

            migrationBuilder.AddColumn<bool>(
                name: "IsUsersPatient",
                table: "UserAssessmentClaimsAudit",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsersPatient",
                table: "UserAssessmentClaims",
                type: "bit",
                nullable: true);
        }
    }
}
