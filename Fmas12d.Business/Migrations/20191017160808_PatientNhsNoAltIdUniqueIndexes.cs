using Microsoft.EntityFrameworkCore.Migrations;

namespace mep.business.Migrations
{
    public partial class PatientNhsNoAltIdUniqueIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Patients_AlternativeIdentifier",
                table: "Patients",
                column: "AlternativeIdentifier",
                unique: true,
                filter: "[AlternativeIdentifier] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_NhsNumber",
                table: "Patients",
                column: "NhsNumber",
                unique: true,
                filter: "[NhsNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_AlternativeIdentifier",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_NhsNumber",
                table: "Patients");
        }
    }
}
