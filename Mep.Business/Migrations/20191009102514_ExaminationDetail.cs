using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mep.business.Migrations
{
    public partial class ExaminationDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExaminationDetailsAudit",
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
                    ExaminationId = table.Column<int>(nullable: false),
                    ExaminationDetailTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationDetailsAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationDetailTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationDetailTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationDetailTypes_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationDetailTypesAudit",
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
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationDetailTypesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ExaminationId = table.Column<int>(nullable: false),
                    ExaminationDetailTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationDetails_ExaminationDetailTypes_ExaminationDetailTypeId",
                        column: x => x.ExaminationDetailTypeId,
                        principalTable: "ExaminationDetailTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExaminationDetails_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExaminationDetails_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDetails_ExaminationDetailTypeId",
                table: "ExaminationDetails",
                column: "ExaminationDetailTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDetails_ExaminationId",
                table: "ExaminationDetails",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDetails_ModifiedByUserId",
                table: "ExaminationDetails",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDetailTypes_ModifiedByUserId",
                table: "ExaminationDetailTypes",
                column: "ModifiedByUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationDetails");

            migrationBuilder.DropTable(
                name: "ExaminationDetailsAudit");

            migrationBuilder.DropTable(
                name: "ExaminationDetailTypesAudit");

            migrationBuilder.DropTable(
                name: "ExaminationDetailTypes");
        }
    }
}
