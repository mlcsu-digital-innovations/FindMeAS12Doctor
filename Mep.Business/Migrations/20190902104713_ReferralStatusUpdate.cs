using Microsoft.EntityFrameworkCore.Migrations;

namespace mep.business.Migrations
{
    public partial class ReferralStatusUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferralStatuses_ReferralStatuses_ReferralStatusId",
                table: "ReferralStatuses");

            migrationBuilder.DropIndex(
                name: "IX_ReferralStatuses_ReferralStatusId",
                table: "ReferralStatuses");

            migrationBuilder.DropColumn(
                name: "ReferralStatusId",
                table: "ReferralStatuses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReferralStatusId",
                table: "ReferralStatuses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReferralStatuses_ReferralStatusId",
                table: "ReferralStatuses",
                column: "ReferralStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReferralStatuses_ReferralStatuses_ReferralStatusId",
                table: "ReferralStatuses",
                column: "ReferralStatusId",
                principalTable: "ReferralStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
