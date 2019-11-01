using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.business.Migrations
{
    public static class InitialSystemUserSeed
    {
        static internal void Seed(MigrationBuilder migrationBuilder)
        {
/* UPDATE THE USERS TABLE TO SET NULLABLE COLUMNS FOR INITIAL SEED */
            migrationBuilder.AlterColumn<int>(
                name: "ModifiedByUserId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "OrganisationId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "ProfileTypeId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: false);                  

/* ADD THE SYSTEM USER */
            migrationBuilder.Sql(
              "INSERT [dbo].[Users] " +
              "([IsActive], [ModifiedAt], [DisplayName], [HasReadTermsAndConditions], [IdentityServerIdentifier]) " +
              "VALUES (1, CAST(N'2019-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), N'System Admin', 1, N'bf673270-2538-4e59-9d26-5b4808fd9ef6')"
            );
/* ADD THE SYSTEM ORGANISATION */
            migrationBuilder.Sql(
              "INSERT [dbo].[Organisations] " +
              "([IsActive], [ModifiedAt], [ModifiedByUserId], [Name], [Description]) " +
              "VALUES (1, CAST(N'2019-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), 1, N'System', N'System Organisation')"
            );
/* ADD THE SYSTEM PROFILE TYPE */
            migrationBuilder.Sql(
              "INSERT [dbo].[ProfileTypes] " +
              "([IsActive], [ModifiedAt], [ModifiedByUserId], [Name], [Description]) " +
              "VALUES (1, CAST(N'2019-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), 1, N'System', N'System Profile')"
            );
/* UPDATE THE SYSTEM USER */
            migrationBuilder.Sql(
              "UPDATE [dbo].[Users] " +
              "SET [ModifiedByUserId] = 1," +
              "[OrganisationId] = 1," +
              "[ProfileTypeId] = 1"
            );

/* UPDATE THE USERS TABLE TO SET THE NON NULLABLE COLUMNS */
            migrationBuilder.AlterColumn<int>(
                name: "ModifiedByUserId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganisationId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProfileTypeId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);            
        }
    }
}