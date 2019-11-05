using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class RefactorDoctorStatusToUserAvailability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorStatuses");

            migrationBuilder.DropTable(
                name: "DoctorStatusesAudit");

            migrationBuilder.CreateTable(
                name: "UserAvailabilitiesAudit",
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
                    ContactDetailId = table.Column<int>(nullable: true),
                    End = table.Column<DateTimeOffset>(nullable: false),
                    Start = table.Column<DateTimeOffset>(nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(8,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Postcode = table.Column<string>(nullable: true),
                    UserAvailabilityStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAvailabilitiesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "UserAvailabilityStatus",
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
                    table.PrimaryKey("PK_UserAvailabilityStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAvailabilityStatus_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAvailabilityStatusesAudit",
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
                    table.PrimaryKey("PK_UserAvailabilityStatusesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "UserAvailabilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    ContactDetailId = table.Column<int>(nullable: true),
                    End = table.Column<DateTimeOffset>(nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(8,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    Postcode = table.Column<string>(nullable: true),
                    Start = table.Column<DateTimeOffset>(nullable: false),
                    UserAvailabilityStatusId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAvailabilities_ContactDetails_ContactDetailId",
                        column: x => x.ContactDetailId,
                        principalTable: "ContactDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAvailabilities_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAvailabilities_UserAvailabilityStatus_UserAvailabilityStatusId",
                        column: x => x.UserAvailabilityStatusId,
                        principalTable: "UserAvailabilityStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAvailabilities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAvailabilities_ContactDetailId",
                table: "UserAvailabilities",
                column: "ContactDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAvailabilities_ModifiedByUserId",
                table: "UserAvailabilities",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAvailabilities_UserAvailabilityStatusId",
                table: "UserAvailabilities",
                column: "UserAvailabilityStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAvailabilities_UserId",
                table: "UserAvailabilities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAvailabilityStatus_ModifiedByUserId",
                table: "UserAvailabilityStatus",
                column: "ModifiedByUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAvailabilities");

            migrationBuilder.DropTable(
                name: "UserAvailabilitiesAudit");

            migrationBuilder.DropTable(
                name: "UserAvailabilityStatusesAudit");

            migrationBuilder.DropTable(
                name: "UserAvailabilityStatus");

            migrationBuilder.CreateTable(
                name: "DoctorStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvailabilityEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AvailabilityStart = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExtendedAvailabilityEnd1 = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ExtendedAvailabilityEnd2 = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ExtendedAvailabilityEnd3 = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ExtendedAvailabilityLatitude1 = table.Column<decimal>(type: "decimal(8,6)", nullable: true),
                    ExtendedAvailabilityLatitude2 = table.Column<decimal>(type: "decimal(8,6)", nullable: true),
                    ExtendedAvailabilityLatitude3 = table.Column<decimal>(type: "decimal(8,6)", nullable: true),
                    ExtendedAvailabilityLongitude1 = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    ExtendedAvailabilityLongitude2 = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    ExtendedAvailabilityLongitude3 = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    ExtendedAvailabilityStart1 = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ExtendedAvailabilityStart2 = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ExtendedAvailabilityStart3 = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(8,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedByUserId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorStatuses_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoctorStatuses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorStatusesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditDuration = table.Column<int>(type: "int", nullable: false),
                    AuditErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditResult = table.Column<int>(type: "int", nullable: false),
                    AuditSuccess = table.Column<bool>(type: "bit", nullable: false),
                    AvailabilityEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AvailabilityStart = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExtendedAvailabilityEnd1 = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ExtendedAvailabilityEnd2 = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ExtendedAvailabilityEnd3 = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ExtendedAvailabilityLatitude1 = table.Column<decimal>(type: "decimal(8,6)", nullable: true),
                    ExtendedAvailabilityLatitude2 = table.Column<decimal>(type: "decimal(8,6)", nullable: true),
                    ExtendedAvailabilityLatitude3 = table.Column<decimal>(type: "decimal(8,6)", nullable: true),
                    ExtendedAvailabilityLongitude1 = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    ExtendedAvailabilityLongitude2 = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    ExtendedAvailabilityLongitude3 = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    ExtendedAvailabilityStart1 = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ExtendedAvailabilityStart2 = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ExtendedAvailabilityStart3 = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(8,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedByUserId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorStatusesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorStatuses_ModifiedByUserId",
                table: "DoctorStatuses",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorStatuses_UserId",
                table: "DoctorStatuses",
                column: "UserId");
        }
    }
}
