using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class AddExportedDateToClaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ExportedDate",
                table: "UserAssessmentClaimsAudit",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ExportedDate",
                table: "UserAssessmentClaims",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExportedDate",
                table: "UserAssessmentClaimsAudit");

            migrationBuilder.DropColumn(
                name: "ExportedDate",
                table: "UserAssessmentClaims");
        }
    }
}
