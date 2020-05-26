using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class UpdateSubjectiveCodeToRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE CcgsAudit SET SubjectiveCode = '' WHERE SubjectiveCode IS NULL");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectiveCode",
                table: "CcgsAudit",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.Sql(@"UPDATE Ccgs SET SubjectiveCode = '' WHERE SubjectiveCode IS NULL");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectiveCode",
                table: "Ccgs",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubjectiveCode",
                table: "CcgsAudit",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectiveCode",
                table: "Ccgs",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10);
        }
    }
}
