using Microsoft.EntityFrameworkCore.Migrations;

namespace mep.business.Migrations
{
    public partial class UserExaminationNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasResponded",
                table: "UserExaminationNotificationsAudit");

            migrationBuilder.DropColumn(
                name: "HasResponded",
                table: "UserExaminationNotifications");

            migrationBuilder.RenameColumn(
                name: "ResponsedAt",
                table: "UserExaminationNotificationsAudit",
                newName: "RespondedAt");

            migrationBuilder.RenameColumn(
                name: "ResponsedAt",
                table: "UserExaminationNotifications",
                newName: "RespondedAt");

            migrationBuilder.AlterColumn<bool>(
                name: "HasAccepted",
                table: "UserExaminationNotificationsAudit",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "HasAccepted",
                table: "UserExaminationNotifications",
                nullable: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RespondedAt",
                table: "UserExaminationNotificationsAudit",
                newName: "ResponsedAt");

            migrationBuilder.RenameColumn(
                name: "RespondedAt",
                table: "UserExaminationNotifications",
                newName: "ResponsedAt");

            migrationBuilder.AlterColumn<bool>(
                name: "HasAccepted",
                table: "UserExaminationNotificationsAudit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasResponded",
                table: "UserExaminationNotificationsAudit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "HasAccepted",
                table: "UserExaminationNotifications",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasResponded",
                table: "UserExaminationNotifications",
                nullable: false,
                defaultValue: false);
        }
    }
}
