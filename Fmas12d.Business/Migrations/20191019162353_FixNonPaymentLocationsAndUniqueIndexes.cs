using Microsoft.EntityFrameworkCore.Migrations;

namespace mep.business.Migrations
{
    public partial class FixNonPaymentLocationsAndUniqueIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NonPaymentLocations_CcgId",
                table: "NonPaymentLocations");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_NonPaymentLocations_CcgId_NonPaymentLocationTypeId",
                table: "NonPaymentLocations",
                columns: new[] { "CcgId", "NonPaymentLocationTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_NonPaymentLocationTypes_Name",
                table: "NonPaymentLocationTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetailTypes_Name",
                table: "ContactDetailTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClaimStatuses_Name",
                table: "ClaimStatuses",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NonPaymentLocationTypes_Name",
                table: "NonPaymentLocationTypes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_NonPaymentLocations_CcgId_NonPaymentLocationTypeId",
                table: "NonPaymentLocations");

            migrationBuilder.DropIndex(
                name: "IX_ContactDetailTypes_Name",
                table: "ContactDetailTypes");

            migrationBuilder.DropIndex(
                name: "IX_ClaimStatuses_Name",
                table: "ClaimStatuses");

            migrationBuilder.CreateIndex(
                name: "IX_NonPaymentLocations_CcgId",
                table: "NonPaymentLocations",
                column: "CcgId");
        }
    }
}
