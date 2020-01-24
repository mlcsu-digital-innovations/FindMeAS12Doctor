using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class RemoveOnCallUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnCallUsers");

            migrationBuilder.DropTable(
                name: "OnCallUsersAudit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OnCallUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateTimeStart = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedByUserId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnCallUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnCallUsers_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OnCallUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OnCallUsersAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditDuration = table.Column<int>(type: "int", nullable: false),
                    AuditErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditResult = table.Column<int>(type: "int", nullable: false),
                    AuditSuccess = table.Column<bool>(type: "bit", nullable: false),
                    DateTimeEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateTimeStart = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedByUserId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnCallUsersAudit", x => x.AuditId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OnCallUsers_ModifiedByUserId",
                table: "OnCallUsers",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OnCallUsers_UserId",
                table: "OnCallUsers",
                column: "UserId");
        }
    }
}
