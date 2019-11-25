using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class RefactorNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasAccepted",
                table: "UserAssessmentNotificationsAudit");

            migrationBuilder.DropColumn(
                name: "RespondedAt",
                table: "UserAssessmentNotificationsAudit");

            migrationBuilder.DropColumn(
                name: "HasAccepted",
                table: "UserAssessmentNotifications");

            migrationBuilder.DropColumn(
                name: "RespondedAt",
                table: "UserAssessmentNotifications");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "SentAt",
                table: "UserAssessmentNotificationsAudit",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "SentAt",
                table: "UserAssessmentNotifications",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasAccepted",
                table: "AssessmentDoctorsAudit",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RespondedAt",
                table: "AssessmentDoctorsAudit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasAccepted",
                table: "AssessmentDoctors",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RespondedAt",
                table: "AssessmentDoctors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentAt",
                table: "UserAssessmentNotificationsAudit");

            migrationBuilder.DropColumn(
                name: "SentAt",
                table: "UserAssessmentNotifications");

            migrationBuilder.DropColumn(
                name: "HasAccepted",
                table: "AssessmentDoctorsAudit");

            migrationBuilder.DropColumn(
                name: "RespondedAt",
                table: "AssessmentDoctorsAudit");

            migrationBuilder.DropColumn(
                name: "HasAccepted",
                table: "AssessmentDoctors");

            migrationBuilder.DropColumn(
                name: "RespondedAt",
                table: "AssessmentDoctors");

            migrationBuilder.AddColumn<bool>(
                name: "HasAccepted",
                table: "UserAssessmentNotificationsAudit",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RespondedAt",
                table: "UserAssessmentNotificationsAudit",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasAccepted",
                table: "UserAssessmentNotifications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RespondedAt",
                table: "UserAssessmentNotifications",
                type: "datetimeoffset",
                nullable: true);
        }
    }
}
