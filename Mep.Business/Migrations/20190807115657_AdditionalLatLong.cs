using Microsoft.EntityFrameworkCore.Migrations;

namespace mep.business.Migrations
{
    public partial class AdditionalLatLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ExtendedAvailabilityLatitude1",
                table: "DoctorStatusesAudit",
                type: "decimal(8,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExtendedAvailabilityLatitude2",
                table: "DoctorStatusesAudit",
                type: "decimal(8,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExtendedAvailabilityLatitude3",
                table: "DoctorStatusesAudit",
                type: "decimal(8,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExtendedAvailabilityLongitude1",
                table: "DoctorStatusesAudit",
                type: "decimal(9,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExtendedAvailabilityLongitude2",
                table: "DoctorStatusesAudit",
                type: "decimal(9,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExtendedAvailabilityLongitude3",
                table: "DoctorStatusesAudit",
                type: "decimal(9,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExtendedAvailabilityLatitude1",
                table: "DoctorStatuses",
                type: "decimal(8,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExtendedAvailabilityLatitude2",
                table: "DoctorStatuses",
                type: "decimal(8,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExtendedAvailabilityLatitude3",
                table: "DoctorStatuses",
                type: "decimal(8,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExtendedAvailabilityLongitude1",
                table: "DoctorStatuses",
                type: "decimal(9,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExtendedAvailabilityLongitude2",
                table: "DoctorStatuses",
                type: "decimal(9,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExtendedAvailabilityLongitude3",
                table: "DoctorStatuses",
                type: "decimal(9,6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtendedAvailabilityLatitude1",
                table: "DoctorStatusesAudit");

            migrationBuilder.DropColumn(
                name: "ExtendedAvailabilityLatitude2",
                table: "DoctorStatusesAudit");

            migrationBuilder.DropColumn(
                name: "ExtendedAvailabilityLatitude3",
                table: "DoctorStatusesAudit");

            migrationBuilder.DropColumn(
                name: "ExtendedAvailabilityLongitude1",
                table: "DoctorStatusesAudit");

            migrationBuilder.DropColumn(
                name: "ExtendedAvailabilityLongitude2",
                table: "DoctorStatusesAudit");

            migrationBuilder.DropColumn(
                name: "ExtendedAvailabilityLongitude3",
                table: "DoctorStatusesAudit");

            migrationBuilder.DropColumn(
                name: "ExtendedAvailabilityLatitude1",
                table: "DoctorStatuses");

            migrationBuilder.DropColumn(
                name: "ExtendedAvailabilityLatitude2",
                table: "DoctorStatuses");

            migrationBuilder.DropColumn(
                name: "ExtendedAvailabilityLatitude3",
                table: "DoctorStatuses");

            migrationBuilder.DropColumn(
                name: "ExtendedAvailabilityLongitude1",
                table: "DoctorStatuses");

            migrationBuilder.DropColumn(
                name: "ExtendedAvailabilityLongitude2",
                table: "DoctorStatuses");

            migrationBuilder.DropColumn(
                name: "ExtendedAvailabilityLongitude3",
                table: "DoctorStatuses");
        }
    }
}
