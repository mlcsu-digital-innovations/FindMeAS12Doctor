using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class UserAssessmentClaimsUpdateColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAssessmentClaims_Users_SelectedByUserId",
                table: "UserAssessmentClaims");

            migrationBuilder.DropIndex(
                name: "IX_UserAssessmentClaims_SelectedByUserId",
                table: "UserAssessmentClaims");

            migrationBuilder.DropColumn(
                name: "HasBeenDeallocated",
                table: "UserAssessmentClaimsAudit");

            migrationBuilder.DropColumn(
                name: "SelectedByUserId",
                table: "UserAssessmentClaimsAudit");

            migrationBuilder.DropColumn(
                name: "HasBeenDeallocated",
                table: "UserAssessmentClaims");

            migrationBuilder.DropColumn(
                name: "SelectedByUserId",
                table: "UserAssessmentClaims");

            migrationBuilder.AlterColumn<string>(
                name: "TravelComments",
                table: "UserAssessmentClaimsAudit",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsersPatient",
                table: "UserAssessmentClaimsAudit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TravelComments",
                table: "UserAssessmentClaims",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsersPatient",
                table: "UserAssessmentClaims",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAssessmentClaims_ClaimReference",
                table: "UserAssessmentClaims",
                column: "ClaimReference",
                unique: true,
                filter: "[ClaimReference] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserAssessmentClaims_ClaimReference",
                table: "UserAssessmentClaims");

            migrationBuilder.DropColumn(
                name: "IsUsersPatient",
                table: "UserAssessmentClaimsAudit");

            migrationBuilder.DropColumn(
                name: "IsUsersPatient",
                table: "UserAssessmentClaims");

            migrationBuilder.AlterColumn<string>(
                name: "TravelComments",
                table: "UserAssessmentClaimsAudit",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeallocated",
                table: "UserAssessmentClaimsAudit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SelectedByUserId",
                table: "UserAssessmentClaimsAudit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "TravelComments",
                table: "UserAssessmentClaims",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeallocated",
                table: "UserAssessmentClaims",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SelectedByUserId",
                table: "UserAssessmentClaims",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserAssessmentClaims_SelectedByUserId",
                table: "UserAssessmentClaims",
                column: "SelectedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssessmentClaims_Users_SelectedByUserId",
                table: "UserAssessmentClaims",
                column: "SelectedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
