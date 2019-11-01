using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mep.business.Migrations
{
    public partial class AddGenderTypeAuditsMany2ManyIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NonPaymentLocationTypes_Ccgs_CcgId",
                table: "NonPaymentLocationTypes");

            migrationBuilder.DropIndex(
                name: "IX_UserSpecialities_SpecialityId",
                table: "UserSpecialities");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserExaminationNotifications_ExaminationId_UserId",
                table: "UserExaminationNotifications");

            migrationBuilder.DropIndex(
                name: "IX_UserExaminationClaims_ExaminationId",
                table: "UserExaminationClaims");

            migrationBuilder.DropIndex(
                name: "IX_NonPaymentLocationTypes_CcgId",
                table: "NonPaymentLocationTypes");

            migrationBuilder.DropColumn(
                name: "CcgId",
                table: "NonPaymentLocationTypes");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserSpecialities_SpecialityId_UserId",
                table: "UserSpecialities",
                columns: new[] { "SpecialityId", "UserId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserExaminationNotifications_ExaminationId_NotificationTextId_UserId",
                table: "UserExaminationNotifications",
                columns: new[] { "ExaminationId", "NotificationTextId", "UserId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserExaminationClaims_ExaminationId_UserId",
                table: "UserExaminationClaims",
                columns: new[] { "ExaminationId", "UserId" });

            migrationBuilder.CreateTable(
                name: "GenderTypesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderTypesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_GmcNumber",
                table: "Users",
                column: "GmcNumber",
                unique: true,
                filter: "[GmcNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdentityServerIdentifier",
                table: "Users",
                column: "IdentityServerIdentifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnsuccessfulExaminationTypes_Name",
                table: "UnsuccessfulExaminationTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Section12ApprovalStatuses_Name",
                table: "Section12ApprovalStatuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReferralStatuses_Name",
                table: "ReferralStatuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileTypes_Name",
                table: "ProfileTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethodTypes_Name",
                table: "PaymentMethodTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_Name",
                table: "Organisations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTexts_Name",
                table: "NotificationTexts",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GenderTypes_Name",
                table: "GenderTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDetailTypes_Name",
                table: "ExaminationDetailTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ccgs_Name",
                table: "Ccgs",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenderTypesAudit");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserSpecialities_SpecialityId_UserId",
                table: "UserSpecialities");

            migrationBuilder.DropIndex(
                name: "IX_Users_GmcNumber",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_IdentityServerIdentifier",
                table: "Users");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserExaminationNotifications_ExaminationId_NotificationTextId_UserId",
                table: "UserExaminationNotifications");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserExaminationClaims_ExaminationId_UserId",
                table: "UserExaminationClaims");

            migrationBuilder.DropIndex(
                name: "IX_UnsuccessfulExaminationTypes_Name",
                table: "UnsuccessfulExaminationTypes");

            migrationBuilder.DropIndex(
                name: "IX_Section12ApprovalStatuses_Name",
                table: "Section12ApprovalStatuses");

            migrationBuilder.DropIndex(
                name: "IX_ReferralStatuses_Name",
                table: "ReferralStatuses");

            migrationBuilder.DropIndex(
                name: "IX_ProfileTypes_Name",
                table: "ProfileTypes");

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethodTypes_Name",
                table: "PaymentMethodTypes");

            migrationBuilder.DropIndex(
                name: "IX_Organisations_Name",
                table: "Organisations");

            migrationBuilder.DropIndex(
                name: "IX_NotificationTexts_Name",
                table: "NotificationTexts");

            migrationBuilder.DropIndex(
                name: "IX_GenderTypes_Name",
                table: "GenderTypes");

            migrationBuilder.DropIndex(
                name: "IX_ExaminationDetailTypes_Name",
                table: "ExaminationDetailTypes");

            migrationBuilder.DropIndex(
                name: "IX_Ccgs_Name",
                table: "Ccgs");

            migrationBuilder.AddColumn<int>(
                name: "CcgId",
                table: "NonPaymentLocationTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserExaminationNotifications_ExaminationId_UserId",
                table: "UserExaminationNotifications",
                columns: new[] { "ExaminationId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSpecialities_SpecialityId",
                table: "UserSpecialities",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExaminationClaims_ExaminationId",
                table: "UserExaminationClaims",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_NonPaymentLocationTypes_CcgId",
                table: "NonPaymentLocationTypes",
                column: "CcgId");

            migrationBuilder.AddForeignKey(
                name: "FK_NonPaymentLocationTypes_Ccgs_CcgId",
                table: "NonPaymentLocationTypes",
                column: "CcgId",
                principalTable: "Ccgs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
