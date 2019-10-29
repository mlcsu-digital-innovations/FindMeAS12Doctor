using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mep.business.Migrations
{
    public partial class ExaminationDoctorsAndAmhp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmhpUserId",
                table: "Examinations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExaminationDoctorStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationDoctorStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationDoctorStatuses_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationDoctorStatusesAudit",
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
                    table.PrimaryKey("PK_ExaminationDoctorStatusesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationDoctors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    AttendanceConfirmedByUserId = table.Column<int>(nullable: true),
                    DoctorUserId = table.Column<int>(nullable: false),
                    ExaminationId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationDoctors", x => x.Id);
                    table.UniqueConstraint("AK_ExaminationDoctors_ExaminationId_DoctorUserId", x => new { x.ExaminationId, x.DoctorUserId });
                    table.ForeignKey(
                        name: "FK_ExaminationDoctors_Users_AttendanceConfirmedByUserId",
                        column: x => x.AttendanceConfirmedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExaminationDoctors_Users_DoctorUserId",
                        column: x => x.DoctorUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExaminationDoctors_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExaminationDoctors_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExaminationDoctors_ExaminationDoctorStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ExaminationDoctorStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationDoctorsAudit",
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
                    AttendanceConfirmedByUserId = table.Column<int>(nullable: true),
                    DoctorUserId = table.Column<int>(nullable: false),
                    ExaminationId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationDoctorsAudit", x => x.AuditId);
                    table.ForeignKey(
                        name: "FK_ExaminationDoctorsAudit_Users_AttendanceConfirmedByUserId",
                        column: x => x.AttendanceConfirmedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExaminationDoctorsAudit_Users_DoctorUserId",
                        column: x => x.DoctorUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExaminationDoctorsAudit_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExaminationDoctorsAudit_ExaminationDoctorStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ExaminationDoctorStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_AmhpUserId",
                table: "Examinations",
                column: "AmhpUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDoctors_AttendanceConfirmedByUserId",
                table: "ExaminationDoctors",
                column: "AttendanceConfirmedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDoctors_DoctorUserId",
                table: "ExaminationDoctors",
                column: "DoctorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDoctors_ModifiedByUserId",
                table: "ExaminationDoctors",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDoctors_StatusId",
                table: "ExaminationDoctors",
                column: "StatusId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDoctorStatuses_ModifiedByUserId",
                table: "ExaminationDoctorStatuses",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDoctorStatuses_Name",
                table: "ExaminationDoctorStatuses",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_Users_AmhpUserId",
                table: "Examinations",
                column: "AmhpUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_Users_AmhpUserId",
                table: "Examinations");

            migrationBuilder.DropTable(
                name: "ExaminationDoctors");

            migrationBuilder.DropTable(
                name: "ExaminationDoctorsAudit");

            migrationBuilder.DropTable(
                name: "ExaminationDoctorStatusesAudit");

            migrationBuilder.DropTable(
                name: "ExaminationDoctorStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Examinations_AmhpUserId",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "AmhpUserId",
                table: "Examinations");
        }
    }
}
