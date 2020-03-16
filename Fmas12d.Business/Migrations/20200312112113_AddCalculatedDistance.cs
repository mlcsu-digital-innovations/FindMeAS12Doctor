using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class AddCalculatedDistance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalculatedDistances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Distance = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    EndLatitude = table.Column<decimal>(type: "decimal(8,6)", nullable: false),
                    EndLongitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    EstimatedJourneyTime = table.Column<int>(nullable: false),
                    StartLatitude = table.Column<decimal>(type: "decimal(8,6)", nullable: false),
                    StartLongitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculatedDistances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalculatedDistances_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalculatedDistances_ModifiedByUserId",
                table: "CalculatedDistances",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatedDistances_StartLatitude_StartLongitude_EndLatitude_EndLongitude",
                table: "CalculatedDistances",
                columns: new[] { "StartLatitude", "StartLongitude", "EndLatitude", "EndLongitude" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculatedDistances");
        }
    }
}
