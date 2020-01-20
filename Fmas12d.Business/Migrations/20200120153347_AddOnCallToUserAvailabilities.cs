using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class AddOnCallToUserAvailabilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "OnCallConfirmationReceivedAt",
                table: "UserAvailabilities",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "OnCallConfirmationSentAt",
                table: "UserAvailabilities",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OnCallIsConfirmed",
                table: "UserAvailabilities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnCallRejectedReason",
                table: "UserAvailabilities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnCallConfirmationReceivedAt",
                table: "UserAvailabilities");

            migrationBuilder.DropColumn(
                name: "OnCallConfirmationSentAt",
                table: "UserAvailabilities");

            migrationBuilder.DropColumn(
                name: "OnCallIsConfirmed",
                table: "UserAvailabilities");

            migrationBuilder.DropColumn(
                name: "OnCallRejectedReason",
                table: "UserAvailabilities");
        }
    }
}
