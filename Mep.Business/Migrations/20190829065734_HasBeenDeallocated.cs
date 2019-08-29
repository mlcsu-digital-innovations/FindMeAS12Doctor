using Microsoft.EntityFrameworkCore.Migrations;

namespace mep.business.Migrations
{
    public partial class HasBeenDeallocated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeallocated",
                table: "UserExaminationClaimsAudit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeallocated",
                table: "UserExaminationClaims",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBeenDeallocated",
                table: "UserExaminationClaimsAudit");

            migrationBuilder.DropColumn(
                name: "HasBeenDeallocated",
                table: "UserExaminationClaims");
        }
    }
}
