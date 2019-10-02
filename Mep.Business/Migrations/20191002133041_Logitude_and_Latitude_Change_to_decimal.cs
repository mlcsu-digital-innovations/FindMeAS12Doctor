using Microsoft.EntityFrameworkCore.Migrations;

namespace mep.business.Migrations
{
    public partial class Logitude_and_Latitude_Change_to_decimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "ContactDetailsAudit",
                type: "decimal(9,6)",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "ContactDetailsAudit",
                type: "decimal(8,6)",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "ContactDetails",
                type: "decimal(9,6)",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "ContactDetails",
                type: "decimal(8,6)",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Longitude",
                table: "ContactDetailsAudit",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)");

            migrationBuilder.AlterColumn<int>(
                name: "Latitude",
                table: "ContactDetailsAudit",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,6)");

            migrationBuilder.AlterColumn<int>(
                name: "Longitude",
                table: "ContactDetails",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)");

            migrationBuilder.AlterColumn<int>(
                name: "Latitude",
                table: "ContactDetails",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,6)");
        }
    }
}
