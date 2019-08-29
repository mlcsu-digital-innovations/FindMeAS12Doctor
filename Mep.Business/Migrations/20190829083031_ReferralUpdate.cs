using Microsoft.EntityFrameworkCore.Migrations;

namespace mep.business.Migrations
{
    public partial class ReferralUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPlannedExamination",
                table: "ReferralsAudit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LeadAmhpUserId",
                table: "ReferralsAudit",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPlannedExamination",
                table: "Referrals",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LeadAmhpUserId",
                table: "Referrals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Referrals_LeadAmhpUserId",
                table: "Referrals",
                column: "LeadAmhpUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Referrals_Users_LeadAmhpUserId",
                table: "Referrals",
                column: "LeadAmhpUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Referrals_Users_LeadAmhpUserId",
                table: "Referrals");

            migrationBuilder.DropIndex(
                name: "IX_Referrals_LeadAmhpUserId",
                table: "Referrals");

            migrationBuilder.DropColumn(
                name: "IsPlannedExamination",
                table: "ReferralsAudit");

            migrationBuilder.DropColumn(
                name: "LeadAmhpUserId",
                table: "ReferralsAudit");

            migrationBuilder.DropColumn(
                name: "IsPlannedExamination",
                table: "Referrals");

            migrationBuilder.DropColumn(
                name: "LeadAmhpUserId",
                table: "Referrals");
        }
    }
}
