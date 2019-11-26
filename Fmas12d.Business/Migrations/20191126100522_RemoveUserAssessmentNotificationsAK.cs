using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class RemoveUserAssessmentNotificationsAK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserAssessmentNotifications_AssessmentId_NotificationTextId_UserId",
                table: "UserAssessmentNotifications");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssessmentNotifications_AssessmentId",
                table: "UserAssessmentNotifications",
                column: "AssessmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserAssessmentNotifications_AssessmentId",
                table: "UserAssessmentNotifications");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserAssessmentNotifications_AssessmentId_NotificationTextId_UserId",
                table: "UserAssessmentNotifications",
                columns: new[] { "AssessmentId", "NotificationTextId", "UserId" });
        }
    }
}
