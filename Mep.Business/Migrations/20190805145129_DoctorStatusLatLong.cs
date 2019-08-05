using Microsoft.EntityFrameworkCore.Migrations;

namespace mep.business.Migrations
{
    public partial class DoctorStatusLatLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "DoctorStatusesAudit",
                type: "decimal(9,6)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "DoctorStatusesAudit",
                type: "decimal(8,6)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "DoctorStatuses",
                type: "decimal(9,6)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "DoctorStatuses",
                type: "decimal(8,6)",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Longitude",
                table: "DoctorStatusesAudit",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)");

            migrationBuilder.AlterColumn<int>(
                name: "Latitude",
                table: "DoctorStatusesAudit",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,6)");

            migrationBuilder.AlterColumn<int>(
                name: "Longitude",
                table: "DoctorStatuses",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)");

            migrationBuilder.AlterColumn<int>(
                name: "Latitude",
                table: "DoctorStatuses",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,6)");
        }
    }
}
