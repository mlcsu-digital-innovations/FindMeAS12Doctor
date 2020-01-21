using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class AddSection12LiveRegister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactDetailCcgAudits",
                table: "ContactDetailCcgAudits");

            migrationBuilder.RenameTable(
                name: "ContactDetailCcgAudits",
                newName: "ContactDetailCcgsAudit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactDetailCcgsAudit",
                table: "ContactDetailCcgsAudit",
                column: "AuditId");

            migrationBuilder.CreateTable(
                name: "Section12LiveRegisters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    ExpiryDate = table.Column<DateTimeOffset>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    GmcNumber = table.Column<int>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section12LiveRegisters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Section12LiveRegisters_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Section12LiveRegistersAudit",
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
                    ExpiryDate = table.Column<DateTimeOffset>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    GmcNumber = table.Column<int>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section12LiveRegistersAudit", x => x.AuditId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Section12LiveRegisters_GmcNumber",
                table: "Section12LiveRegisters",
                column: "GmcNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Section12LiveRegisters_ModifiedByUserId",
                table: "Section12LiveRegisters",
                column: "ModifiedByUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Section12LiveRegisters");

            migrationBuilder.DropTable(
                name: "Section12LiveRegistersAudit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactDetailCcgsAudit",
                table: "ContactDetailCcgsAudit");

            migrationBuilder.RenameTable(
                name: "ContactDetailCcgsAudit",
                newName: "ContactDetailCcgAudits");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactDetailCcgAudits",
                table: "ContactDetailCcgAudits",
                column: "AuditId");
        }
    }
}
