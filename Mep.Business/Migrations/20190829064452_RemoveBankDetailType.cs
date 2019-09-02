using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mep.business.Migrations
{
    public partial class RemoveBankDetailType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankDetails_BankDetailTypes_BankDetailTypeId",
                table: "BankDetails");

            migrationBuilder.DropTable(
                name: "BankDetailTypes");

            migrationBuilder.DropTable(
                name: "BankDetailTypesAudit");

            migrationBuilder.DropIndex(
                name: "IX_BankDetails_BankDetailTypeId",
                table: "BankDetails");

            migrationBuilder.DropColumn(
                name: "BankDetailTypeId",
                table: "BankDetailsAudit");

            migrationBuilder.DropColumn(
                name: "BankDetailTypeId",
                table: "BankDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankDetailTypeId",
                table: "BankDetailsAudit",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BankDetailTypeId",
                table: "BankDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BankDetailTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDetailTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankDetailTypes_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankDetailTypesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDetailTypesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankDetails_BankDetailTypeId",
                table: "BankDetails",
                column: "BankDetailTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BankDetailTypes_ModifiedByUserId",
                table: "BankDetailTypes",
                column: "ModifiedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankDetails_BankDetailTypes_BankDetailTypeId",
                table: "BankDetails",
                column: "BankDetailTypeId",
                principalTable: "BankDetailTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
