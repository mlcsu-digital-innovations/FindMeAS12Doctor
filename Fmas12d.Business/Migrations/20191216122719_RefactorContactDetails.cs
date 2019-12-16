using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class RefactorContactDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ContactDetails_CcgId_ContactDetailTypeId_UserId",
                table: "ContactDetails");

            migrationBuilder.DropIndex(
                name: "IX_ContactDetails_ContactDetailTypeId",
                table: "ContactDetails");

            migrationBuilder.AlterColumn<long>(
                name: "TelephoneNumber",
                table: "ContactDetailsAudit",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TelephoneNumber",
                table: "ContactDetails",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CcgId",
                table: "ContactDetails",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ContactDetails_ContactDetailTypeId_UserId",
                table: "ContactDetails",
                columns: new[] { "ContactDetailTypeId", "UserId" });

            migrationBuilder.CreateTable(
                name: "ContactDetailCcgAudits",
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
                    CcgId = table.Column<int>(nullable: false),
                    ContactDetailTypeId = table.Column<int>(nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 100, nullable: true),
                    TelephoneNumber = table.Column<long>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetailCcgAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetailCcgs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    ContactDetailTypeId = table.Column<int>(nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 100, nullable: true),
                    TelephoneNumber = table.Column<long>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetailCcgs", x => x.Id);
                    table.UniqueConstraint("AK_ContactDetailCcgs_CcgId_ContactDetailTypeId_UserId", x => new { x.CcgId, x.ContactDetailTypeId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ContactDetailCcgs_Ccgs_CcgId",
                        column: x => x.CcgId,
                        principalTable: "Ccgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactDetailCcgs_ContactDetailTypes_ContactDetailTypeId",
                        column: x => x.ContactDetailTypeId,
                        principalTable: "ContactDetailTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactDetailCcgs_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactDetailCcgs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_CcgId",
                table: "ContactDetails",
                column: "CcgId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetailCcgs_ContactDetailTypeId",
                table: "ContactDetailCcgs",
                column: "ContactDetailTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetailCcgs_ModifiedByUserId",
                table: "ContactDetailCcgs",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetailCcgs_UserId",
                table: "ContactDetailCcgs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactDetailCcgAudits");

            migrationBuilder.DropTable(
                name: "ContactDetailCcgs");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ContactDetails_ContactDetailTypeId_UserId",
                table: "ContactDetails");

            migrationBuilder.DropIndex(
                name: "IX_ContactDetails_CcgId",
                table: "ContactDetails");

            migrationBuilder.AlterColumn<int>(
                name: "TelephoneNumber",
                table: "ContactDetailsAudit",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TelephoneNumber",
                table: "ContactDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CcgId",
                table: "ContactDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ContactDetails_CcgId_ContactDetailTypeId_UserId",
                table: "ContactDetails",
                columns: new[] { "CcgId", "ContactDetailTypeId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_ContactDetailTypeId",
                table: "ContactDetails",
                column: "ContactDetailTypeId");
        }
    }
}
