using Microsoft.EntityFrameworkCore.Migrations;

namespace mep.business.Migrations
{
    public partial class CcgCodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LongCode",
                table: "CcgsAudit",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortCode",
                table: "CcgsAudit",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LongCode",
                table: "Ccgs",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortCode",
                table: "Ccgs",
                maxLength: 5,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongCode",
                table: "CcgsAudit");

            migrationBuilder.DropColumn(
                name: "ShortCode",
                table: "CcgsAudit");

            migrationBuilder.DropColumn(
                name: "LongCode",
                table: "Ccgs");

            migrationBuilder.DropColumn(
                name: "ShortCode",
                table: "Ccgs");
        }
    }
}
