using Microsoft.EntityFrameworkCore.Migrations;

namespace mep.business.Migrations
{
    public partial class examination_update_nonpaymentlocationid_to_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NonPaymentLocationId",
                table: "ExaminationsAudit",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "NonPaymentLocationId",
                table: "Examinations",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NonPaymentLocationId",
                table: "ExaminationsAudit",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NonPaymentLocationId",
                table: "Examinations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
