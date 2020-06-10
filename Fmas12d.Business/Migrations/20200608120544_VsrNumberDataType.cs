using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class VsrNumberDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "VsrNumber",
                table: "BankDetailsAudit",
                type: "decimal(10,0)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "VsrNumber",
                table: "BankDetails",
                type: "decimal(10,0)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VsrNumber",
                table: "BankDetailsAudit",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VsrNumber",
                table: "BankDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,0)",
                oldNullable: true);
        }
    }
}
