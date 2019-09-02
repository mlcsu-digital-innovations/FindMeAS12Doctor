using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mep.business.Migrations
{
    public partial class GenderType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenderTypeId",
                table: "UsersAudit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GenderTypeId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GenderTypeId",
                table: "ExaminationsAudit",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PreferredDoctorGenderTypeId",
                table: "Examinations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GenderTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenderTypes_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderTypeId",
                table: "Users",
                column: "GenderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_PreferredDoctorGenderTypeId",
                table: "Examinations",
                column: "PreferredDoctorGenderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GenderTypes_ModifiedByUserId",
                table: "GenderTypes",
                column: "ModifiedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_GenderTypes_PreferredDoctorGenderTypeId",
                table: "Examinations",
                column: "PreferredDoctorGenderTypeId",
                principalTable: "GenderTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_GenderTypes_GenderTypeId",
                table: "Users",
                column: "GenderTypeId",
                principalTable: "GenderTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_GenderTypes_PreferredDoctorGenderTypeId",
                table: "Examinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_GenderTypes_GenderTypeId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "GenderTypes");

            migrationBuilder.DropIndex(
                name: "IX_Users_GenderTypeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Examinations_PreferredDoctorGenderTypeId",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "GenderTypeId",
                table: "UsersAudit");

            migrationBuilder.DropColumn(
                name: "GenderTypeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GenderTypeId",
                table: "ExaminationsAudit");

            migrationBuilder.DropColumn(
                name: "PreferredDoctorGenderTypeId",
                table: "Examinations");
        }
    }
}
