using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class AddSubjectiveCodeToCcg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubjectiveCode",
                table: "CcgsAudit",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubjectiveCode",
                table: "Ccgs",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectiveCode",
                table: "CcgsAudit");

            migrationBuilder.DropColumn(
                name: "SubjectiveCode",
                table: "Ccgs");
        }
    }
}
