using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class AddEmailNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {            
            migrationBuilder.CreateTable(
                name: "NotificationEmails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    MessageTemplate = table.Column<string>(nullable: false),
                    SubjectTemplate = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationEmails_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationEmailsAudit",
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
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    MessageTemplate = table.Column<string>(nullable: false),
                    SubjectTemplate = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationEmailsAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "UserNotificationEmailAudit",
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
                    ToAddress = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    EmailContent = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTimeOffset>(nullable: false),
                    DateSent = table.Column<DateTimeOffset>(nullable: true),
                    NotificationEmailId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotificationEmailAudit", x => x.AuditId);
                    table.ForeignKey(
                        name: "FK_UserNotificationEmailAudit_NotificationEmails_NotificationEmailId",
                        column: x => x.NotificationEmailId,
                        principalTable: "NotificationEmails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserNotificationEmailAudit_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserNotificationEmails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    ToAddress = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    EmailContent = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTimeOffset>(nullable: false),
                    DateSent = table.Column<DateTimeOffset>(nullable: true),
                    NotificationEmailId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotificationEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNotificationEmails_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserNotificationEmails_NotificationEmails_NotificationEmailId",
                        column: x => x.NotificationEmailId,
                        principalTable: "NotificationEmails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserNotificationEmails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationEmails_ModifiedByUserId",
                table: "NotificationEmails",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationEmails_Name",
                table: "NotificationEmails",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserNotificationEmailAudit_NotificationEmailId",
                table: "UserNotificationEmailAudit",
                column: "NotificationEmailId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotificationEmailAudit_UserId",
                table: "UserNotificationEmailAudit",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotificationEmails_ModifiedByUserId",
                table: "UserNotificationEmails",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotificationEmails_NotificationEmailId",
                table: "UserNotificationEmails",
                column: "NotificationEmailId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotificationEmails_UserId",
                table: "UserNotificationEmails",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationEmailsAudit");

            migrationBuilder.DropTable(
                name: "UserNotificationEmailAudit");

            migrationBuilder.DropTable(
                name: "UserNotificationEmails");

            migrationBuilder.DropTable(
                name: "NotificationEmails");

        }
    }
}
