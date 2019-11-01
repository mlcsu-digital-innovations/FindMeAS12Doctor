using Microsoft.EntityFrameworkCore.Migrations;

namespace mep.business.Migrations
{
    public partial class RemoveExamDrAuditIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationDoctorsAudit_Users_AttendanceConfirmedByUserId",
                table: "ExaminationDoctorsAudit");

            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationDoctorsAudit_Users_DoctorUserId",
                table: "ExaminationDoctorsAudit");

            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationDoctorsAudit_Examinations_ExaminationId",
                table: "ExaminationDoctorsAudit");

            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationDoctorsAudit_ExaminationDoctorStatuses_StatusId",
                table: "ExaminationDoctorsAudit");

            migrationBuilder.DropIndex(
                name: "IX_ExaminationDoctorsAudit_AttendanceConfirmedByUserId",
                table: "ExaminationDoctorsAudit");

            migrationBuilder.DropIndex(
                name: "IX_ExaminationDoctorsAudit_DoctorUserId",
                table: "ExaminationDoctorsAudit");

            migrationBuilder.DropIndex(
                name: "IX_ExaminationDoctorsAudit_ExaminationId",
                table: "ExaminationDoctorsAudit");

            migrationBuilder.DropIndex(
                name: "IX_ExaminationDoctorsAudit_StatusId",
                table: "ExaminationDoctorsAudit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDoctorsAudit_AttendanceConfirmedByUserId",
                table: "ExaminationDoctorsAudit",
                column: "AttendanceConfirmedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDoctorsAudit_DoctorUserId",
                table: "ExaminationDoctorsAudit",
                column: "DoctorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDoctorsAudit_ExaminationId",
                table: "ExaminationDoctorsAudit",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDoctorsAudit_StatusId",
                table: "ExaminationDoctorsAudit",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationDoctorsAudit_Users_AttendanceConfirmedByUserId",
                table: "ExaminationDoctorsAudit",
                column: "AttendanceConfirmedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationDoctorsAudit_Users_DoctorUserId",
                table: "ExaminationDoctorsAudit",
                column: "DoctorUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationDoctorsAudit_Examinations_ExaminationId",
                table: "ExaminationDoctorsAudit",
                column: "ExaminationId",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationDoctorsAudit_ExaminationDoctorStatuses_StatusId",
                table: "ExaminationDoctorsAudit",
                column: "StatusId",
                principalTable: "ExaminationDoctorStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
