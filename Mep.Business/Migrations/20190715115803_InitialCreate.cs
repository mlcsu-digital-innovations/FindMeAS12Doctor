using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mep.business.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CcgAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    CostCentre = table.Column<int>(nullable: false),
                    FailedExamPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPaymentApprovalRequired = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    SuccessfulPencePerMile = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnsuccessfulPencePerMile = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CcgAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "Ccgs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    CostCentre = table.Column<int>(nullable: false),
                    FailedExamPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPaymentApprovalRequired = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    SuccessfulPencePerMile = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnsuccessfulPencePerMile = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ccgs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClaimStatusAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimStatusAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "ClaimStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetailTypeAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetailTypeAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetailTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetailTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorStatusAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    AvailabilityStart = table.Column<DateTimeOffset>(nullable: false),
                    AvailabilityEnd = table.Column<DateTimeOffset>(nullable: false),
                    ExtendedAvailabilityEnd1 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityStart1 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityEnd2 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityStart2 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityEnd3 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityStart3 = table.Column<DateTimeOffset>(nullable: true),
                    Latitude = table.Column<int>(nullable: false),
                    Longitude = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorStatusAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "NonPaymentLocationAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    NonPaymentLocationTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonPaymentLocationAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "NonPaymentLocationTypeAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonPaymentLocationTypeAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTextAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    MessageTemplate = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTextAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTexts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    MessageTemplate = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OnCallUserAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    DateTimeEnd = table.Column<DateTimeOffset>(nullable: false),
                    DateTimeStart = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnCallUserAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "OrganisationAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    AlternativeIdentifier = table.Column<string>(maxLength: 200, nullable: true),
                    CcgId = table.Column<int>(nullable: true),
                    GpPracticeId = table.Column<int>(nullable: true),
                    NhsNumber = table.Column<int>(nullable: true),
                    ResidentialPostcode = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethodAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    PaymentMethodTypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethodAudits", x => x.AuditId);
                    table.UniqueConstraint("AK_PaymentMethodAudits_CcgId_PaymentMethodTypeId_UserId", x => new { x.CcgId, x.PaymentMethodTypeId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethodTypeAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethodTypeAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethodTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethodTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentRuleAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Criteria = table.Column<string>(maxLength: 2000, nullable: false),
                    PaymentRuleSetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRuleAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentRuleSetAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    DateTimeFrom = table.Column<DateTimeOffset>(nullable: false),
                    DateTimeTo = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRuleSetAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "ProfileTypeAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileTypeAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "ProfileTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReferralAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    ReferralStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferralAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "ReferralStatusAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferralStatusAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "ReferralStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    ReferralStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferralStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferralStatuses_ReferralStatuses_ReferralStatusId",
                        column: x => x.ReferralStatusId,
                        principalTable: "ReferralStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Section12ApprovalStatusAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section12ApprovalStatusAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "Section12ApprovalStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section12ApprovalStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    FinanceMileageSubjectiveCode = table.Column<int>(nullable: true),
                    FinanceSubjectiveCode = table.Column<int>(nullable: true),
                    LevelOfUrgencyTimescaleMinutes = table.Column<int>(nullable: false),
                    NonSection12Payment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Section12Payment = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecialityAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    FinanceMileageSubjectiveCode = table.Column<int>(nullable: true),
                    FinanceSubjectiveCode = table.Column<int>(nullable: true),
                    LevelOfUrgencyTimescaleMinutes = table.Column<int>(nullable: false),
                    NonSection12Payment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Section12Payment = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialityAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "UnsuccessfulExaminationTypeAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnsuccessfulExaminationTypeAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "UnsuccessfulExaminationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnsuccessfulExaminationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    GmcNumber = table.Column<int>(nullable: true),
                    HasReadTermsAndConditions = table.Column<bool>(nullable: false),
                    IdentityServerIdentifier = table.Column<string>(maxLength: 50, nullable: false),
                    OrganisationId = table.Column<int>(nullable: false),
                    ProfileTypeId = table.Column<int>(nullable: false),
                    Section12ApprovalStatusId = table.Column<int>(nullable: true),
                    Section12ExpiryDate = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "UserExaminationClaimAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    ClaimReference = table.Column<int>(nullable: true),
                    ClaimStatusId = table.Column<int>(nullable: true),
                    ExaminationId = table.Column<int>(nullable: false),
                    ExaminationPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsAttendanceConfirmed = table.Column<bool>(nullable: false),
                    IsClaimable = table.Column<bool>(nullable: true),
                    Mileage = table.Column<int>(nullable: true),
                    MileagePayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentDate = table.Column<DateTimeOffset>(nullable: true),
                    SelectedByUserId = table.Column<int>(nullable: false),
                    StartPostcode = table.Column<string>(maxLength: 10, nullable: false),
                    TravelComments = table.Column<string>(maxLength: 2000, nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExaminationClaimAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "UserExaminationNotificationAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    ExaminationId = table.Column<int>(nullable: false),
                    HasAccepted = table.Column<bool>(nullable: false),
                    HasResponded = table.Column<bool>(nullable: false),
                    NotificationTextId = table.Column<int>(nullable: false),
                    ResponsedAt = table.Column<DateTimeOffset>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExaminationNotificationAudits", x => x.AuditId);
                    table.UniqueConstraint("AK_UserExaminationNotificationAudits_ExaminationId_UserId", x => new { x.ExaminationId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "UserSpecialitieAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    SpecialityId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSpecialitieAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "BankDetailsAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    AccountNumber = table.Column<int>(nullable: false),
                    BankName = table.Column<string>(maxLength: 200, nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    NameOnAccount = table.Column<string>(maxLength: 200, nullable: false),
                    SortCode = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    VsrNumber = table.Column<int>(nullable: false),
                    CcgAuditAuditId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDetailsAudit", x => x.AuditId);
                    table.UniqueConstraint("AK_BankDetailsAudit_CcgId_UserId", x => new { x.CcgId, x.UserId });
                    table.ForeignKey(
                        name: "FK_BankDetailsAudit_CcgAudits_CcgAuditAuditId",
                        column: x => x.CcgAuditAuditId,
                        principalTable: "CcgAudits",
                        principalColumn: "AuditId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetailAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Address1 = table.Column<string>(maxLength: 200, nullable: false),
                    Address2 = table.Column<string>(maxLength: 200, nullable: true),
                    Address3 = table.Column<string>(maxLength: 200, nullable: true),
                    CcgId = table.Column<int>(nullable: false),
                    ContactDetailTypeId = table.Column<int>(nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 100, nullable: true),
                    Latitude = table.Column<int>(nullable: true),
                    Longitude = table.Column<int>(nullable: true),
                    Postcode = table.Column<string>(maxLength: 10, nullable: true),
                    TelephoneNumber = table.Column<int>(nullable: true),
                    Town = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    CcgAuditAuditId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetailAudits", x => x.AuditId);
                    table.UniqueConstraint("AK_ContactDetailAudits_CcgId_ContactDetailTypeId_UserId", x => new { x.CcgId, x.ContactDetailTypeId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ContactDetailAudits_CcgAudits_CcgAuditAuditId",
                        column: x => x.CcgAuditAuditId,
                        principalTable: "CcgAudits",
                        principalColumn: "AuditId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Address1 = table.Column<string>(maxLength: 200, nullable: false),
                    Address2 = table.Column<string>(nullable: true),
                    Address3 = table.Column<string>(nullable: true),
                    Address4 = table.Column<string>(nullable: true),
                    CcgId = table.Column<int>(nullable: false),
                    CompletedByUserId = table.Column<int>(nullable: true),
                    CompletedTime = table.Column<DateTimeOffset>(nullable: true),
                    CompletionConfirmationByUserId = table.Column<int>(nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    IsSuccessful = table.Column<bool>(nullable: true),
                    MeetingArrangementComment = table.Column<string>(maxLength: 2000, nullable: true),
                    MustBeCompletedBy = table.Column<DateTimeOffset>(nullable: false),
                    NonPaymentLocationId = table.Column<int>(nullable: false),
                    Postcode = table.Column<string>(maxLength: 10, nullable: false),
                    ReferralId = table.Column<int>(nullable: false),
                    ScheduledTime = table.Column<DateTimeOffset>(nullable: false),
                    SpecialityTypeId = table.Column<int>(nullable: false),
                    UnsuccesfulExaminationTypeId = table.Column<int>(nullable: true),
                    CcgAuditAuditId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationAudits", x => x.AuditId);
                    table.ForeignKey(
                        name: "FK_ExaminationAudits_CcgAudits_CcgAuditAuditId",
                        column: x => x.CcgAuditAuditId,
                        principalTable: "CcgAudits",
                        principalColumn: "AuditId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GpPracticeAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    GpPracticeCode = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Postcode = table.Column<string>(maxLength: 10, nullable: false),
                    CcgAuditAuditId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpPracticeAudits", x => x.AuditId);
                    table.ForeignKey(
                        name: "FK_GpPracticeAudits_CcgAudits_CcgAuditAuditId",
                        column: x => x.CcgAuditAuditId,
                        principalTable: "CcgAudits",
                        principalColumn: "AuditId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GpPractices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    GpPracticeCode = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Postcode = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpPractices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GpPractices_Ccgs_CcgId",
                        column: x => x.CcgId,
                        principalTable: "Ccgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NonPaymentLocationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    CcgId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonPaymentLocationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NonPaymentLocationTypes_Ccgs_CcgId",
                        column: x => x.CcgId,
                        principalTable: "Ccgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentRuleSets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    DateTimeFrom = table.Column<DateTimeOffset>(nullable: false),
                    DateTimeTo = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRuleSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentRuleSets_Ccgs_CcgId",
                        column: x => x.CcgId,
                        principalTable: "Ccgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    GmcNumber = table.Column<int>(nullable: true),
                    HasReadTermsAndConditions = table.Column<bool>(nullable: false),
                    IdentityServerIdentifier = table.Column<string>(maxLength: 50, nullable: false),
                    OrganisationId = table.Column<int>(nullable: false),
                    ProfileTypeId = table.Column<int>(nullable: false),
                    Section12ApprovalStatusId = table.Column<int>(nullable: true),
                    Section12ExpiryDate = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_ProfileTypes_ProfileTypeId",
                        column: x => x.ProfileTypeId,
                        principalTable: "ProfileTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Section12ApprovalStatuses_Section12ApprovalStatusId",
                        column: x => x.Section12ApprovalStatusId,
                        principalTable: "Section12ApprovalStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    AlternativeIdentifier = table.Column<string>(maxLength: 200, nullable: true),
                    CcgId = table.Column<int>(nullable: true),
                    GpPracticeId = table.Column<int>(nullable: true),
                    NhsNumber = table.Column<int>(nullable: true),
                    ResidentialPostcode = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Ccgs_CcgId",
                        column: x => x.CcgId,
                        principalTable: "Ccgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patients_GpPractices_GpPracticeId",
                        column: x => x.GpPracticeId,
                        principalTable: "GpPractices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NonPaymentLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    NonPaymentLocationTypeId = table.Column<int>(nullable: false),
                    NonPaymentLocationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonPaymentLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NonPaymentLocations_Ccgs_CcgId",
                        column: x => x.CcgId,
                        principalTable: "Ccgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NonPaymentLocations_NonPaymentLocations_NonPaymentLocationId",
                        column: x => x.NonPaymentLocationId,
                        principalTable: "NonPaymentLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NonPaymentLocations_NonPaymentLocationTypes_NonPaymentLocationTypeId",
                        column: x => x.NonPaymentLocationTypeId,
                        principalTable: "NonPaymentLocationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentRules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Criteria = table.Column<string>(maxLength: 2000, nullable: false),
                    PaymentRuleSetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentRules_PaymentRuleSets_PaymentRuleSetId",
                        column: x => x.PaymentRuleSetId,
                        principalTable: "PaymentRuleSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    AccountNumber = table.Column<int>(nullable: false),
                    BankName = table.Column<string>(maxLength: 200, nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    NameOnAccount = table.Column<string>(maxLength: 200, nullable: false),
                    SortCode = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    VsrNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDetails", x => x.Id);
                    table.UniqueConstraint("AK_BankDetails_CcgId_UserId", x => new { x.CcgId, x.UserId });
                    table.ForeignKey(
                        name: "FK_BankDetails_Ccgs_CcgId",
                        column: x => x.CcgId,
                        principalTable: "Ccgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Address1 = table.Column<string>(maxLength: 200, nullable: false),
                    Address2 = table.Column<string>(maxLength: 200, nullable: true),
                    Address3 = table.Column<string>(maxLength: 200, nullable: true),
                    CcgId = table.Column<int>(nullable: false),
                    ContactDetailTypeId = table.Column<int>(nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 100, nullable: true),
                    Latitude = table.Column<int>(nullable: true),
                    Longitude = table.Column<int>(nullable: true),
                    Postcode = table.Column<string>(maxLength: 10, nullable: true),
                    TelephoneNumber = table.Column<int>(nullable: true),
                    Town = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetails", x => x.Id);
                    table.UniqueConstraint("AK_ContactDetails_CcgId_ContactDetailTypeId_UserId", x => new { x.CcgId, x.ContactDetailTypeId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ContactDetails_Ccgs_CcgId",
                        column: x => x.CcgId,
                        principalTable: "Ccgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactDetails_ContactDetailTypes_ContactDetailTypeId",
                        column: x => x.ContactDetailTypeId,
                        principalTable: "ContactDetailTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    AvailabilityStart = table.Column<DateTimeOffset>(nullable: false),
                    AvailabilityEnd = table.Column<DateTimeOffset>(nullable: false),
                    ExtendedAvailabilityEnd1 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityStart1 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityEnd2 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityStart2 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityEnd3 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityStart3 = table.Column<DateTimeOffset>(nullable: true),
                    Latitude = table.Column<int>(nullable: false),
                    Longitude = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorStatuses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OnCallUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    DateTimeEnd = table.Column<DateTimeOffset>(nullable: false),
                    DateTimeStart = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnCallUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnCallUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    PaymentMethodTypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                    table.UniqueConstraint("AK_PaymentMethods_CcgId_PaymentMethodTypeId_UserId", x => new { x.CcgId, x.PaymentMethodTypeId, x.UserId });
                    table.ForeignKey(
                        name: "FK_PaymentMethods_Ccgs_CcgId",
                        column: x => x.CcgId,
                        principalTable: "Ccgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentMethods_PaymentMethodTypes_PaymentMethodTypeId",
                        column: x => x.PaymentMethodTypeId,
                        principalTable: "PaymentMethodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentMethods_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserSpecialities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    SpecialityId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSpecialities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSpecialities_Specialities_SpecialityId",
                        column: x => x.SpecialityId,
                        principalTable: "Specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSpecialities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Referrals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    ReferralStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referrals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Referrals_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Referrals_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Referrals_ReferralStatuses_ReferralStatusId",
                        column: x => x.ReferralStatusId,
                        principalTable: "ReferralStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Examinations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Address1 = table.Column<string>(maxLength: 200, nullable: false),
                    Address2 = table.Column<string>(nullable: true),
                    Address3 = table.Column<string>(nullable: true),
                    Address4 = table.Column<string>(nullable: true),
                    CcgId = table.Column<int>(nullable: false),
                    CompletedByUserId = table.Column<int>(nullable: true),
                    CompletedTime = table.Column<DateTimeOffset>(nullable: true),
                    CompletionConfirmationByUserId = table.Column<int>(nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    IsSuccessful = table.Column<bool>(nullable: true),
                    MeetingArrangementComment = table.Column<string>(maxLength: 2000, nullable: true),
                    MustBeCompletedBy = table.Column<DateTimeOffset>(nullable: false),
                    NonPaymentLocationId = table.Column<int>(nullable: false),
                    Postcode = table.Column<string>(maxLength: 10, nullable: false),
                    ReferralId = table.Column<int>(nullable: false),
                    ScheduledTime = table.Column<DateTimeOffset>(nullable: false),
                    SpecialityTypeId = table.Column<int>(nullable: false),
                    SpecialityId = table.Column<int>(nullable: true),
                    UnsuccesfulExaminationTypeId = table.Column<int>(nullable: true),
                    UnsuccessfulExaminationTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examinations_Ccgs_CcgId",
                        column: x => x.CcgId,
                        principalTable: "Ccgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_Users_CompletedByUserId",
                        column: x => x.CompletedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_Users_CompletionConfirmationByUserId",
                        column: x => x.CompletionConfirmationByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_NonPaymentLocations_NonPaymentLocationId",
                        column: x => x.NonPaymentLocationId,
                        principalTable: "NonPaymentLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_Referrals_ReferralId",
                        column: x => x.ReferralId,
                        principalTable: "Referrals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_Specialities_SpecialityId",
                        column: x => x.SpecialityId,
                        principalTable: "Specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_UnsuccessfulExaminationTypes_UnsuccessfulExaminationTypeId",
                        column: x => x.UnsuccessfulExaminationTypeId,
                        principalTable: "UnsuccessfulExaminationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserExaminationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    ClaimReference = table.Column<int>(nullable: true),
                    ClaimStatusId = table.Column<int>(nullable: true),
                    ExaminationId = table.Column<int>(nullable: false),
                    ExaminationPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsAttendanceConfirmed = table.Column<bool>(nullable: false),
                    IsClaimable = table.Column<bool>(nullable: true),
                    Mileage = table.Column<int>(nullable: true),
                    MileagePayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentDate = table.Column<DateTimeOffset>(nullable: true),
                    SelectedByUserId = table.Column<int>(nullable: false),
                    StartPostcode = table.Column<string>(maxLength: 10, nullable: false),
                    TravelComments = table.Column<string>(maxLength: 2000, nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExaminationClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserExaminationClaims_ClaimStatuses_ClaimStatusId",
                        column: x => x.ClaimStatusId,
                        principalTable: "ClaimStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserExaminationClaims_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserExaminationClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserExaminationNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    ExaminationId = table.Column<int>(nullable: false),
                    HasAccepted = table.Column<bool>(nullable: false),
                    HasResponded = table.Column<bool>(nullable: false),
                    NotificationTextId = table.Column<int>(nullable: false),
                    ResponsedAt = table.Column<DateTimeOffset>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExaminationNotifications", x => x.Id);
                    table.UniqueConstraint("AK_UserExaminationNotifications_ExaminationId_UserId", x => new { x.ExaminationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserExaminationNotifications_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserExaminationNotifications_NotificationTexts_NotificationTextId",
                        column: x => x.NotificationTextId,
                        principalTable: "NotificationTexts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserExaminationNotifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankDetails_UserId",
                table: "BankDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BankDetailsAudit_CcgAuditAuditId",
                table: "BankDetailsAudit",
                column: "CcgAuditAuditId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetailAudits_CcgAuditAuditId",
                table: "ContactDetailAudits",
                column: "CcgAuditAuditId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_ContactDetailTypeId",
                table: "ContactDetails",
                column: "ContactDetailTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_UserId",
                table: "ContactDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorStatuses_UserId",
                table: "DoctorStatuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationAudits_CcgAuditAuditId",
                table: "ExaminationAudits",
                column: "CcgAuditAuditId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_CcgId",
                table: "Examinations",
                column: "CcgId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_CompletedByUserId",
                table: "Examinations",
                column: "CompletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_CompletionConfirmationByUserId",
                table: "Examinations",
                column: "CompletionConfirmationByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_CreatedByUserId",
                table: "Examinations",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_NonPaymentLocationId",
                table: "Examinations",
                column: "NonPaymentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_ReferralId",
                table: "Examinations",
                column: "ReferralId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_SpecialityId",
                table: "Examinations",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_UnsuccessfulExaminationTypeId",
                table: "Examinations",
                column: "UnsuccessfulExaminationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GpPracticeAudits_CcgAuditAuditId",
                table: "GpPracticeAudits",
                column: "CcgAuditAuditId");

            migrationBuilder.CreateIndex(
                name: "IX_GpPractices_CcgId",
                table: "GpPractices",
                column: "CcgId");

            migrationBuilder.CreateIndex(
                name: "IX_NonPaymentLocations_CcgId",
                table: "NonPaymentLocations",
                column: "CcgId");

            migrationBuilder.CreateIndex(
                name: "IX_NonPaymentLocations_NonPaymentLocationId",
                table: "NonPaymentLocations",
                column: "NonPaymentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_NonPaymentLocations_NonPaymentLocationTypeId",
                table: "NonPaymentLocations",
                column: "NonPaymentLocationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NonPaymentLocationTypes_CcgId",
                table: "NonPaymentLocationTypes",
                column: "CcgId");

            migrationBuilder.CreateIndex(
                name: "IX_OnCallUsers_UserId",
                table: "OnCallUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_CcgId",
                table: "Patients",
                column: "CcgId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_GpPracticeId",
                table: "Patients",
                column: "GpPracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_PaymentMethodTypeId",
                table: "PaymentMethods",
                column: "PaymentMethodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_UserId",
                table: "PaymentMethods",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRules_PaymentRuleSetId",
                table: "PaymentRules",
                column: "PaymentRuleSetId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRuleSets_CcgId",
                table: "PaymentRuleSets",
                column: "CcgId");

            migrationBuilder.CreateIndex(
                name: "IX_Referrals_CreatedByUserId",
                table: "Referrals",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Referrals_PatientId",
                table: "Referrals",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Referrals_ReferralStatusId",
                table: "Referrals",
                column: "ReferralStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferralStatuses_ReferralStatusId",
                table: "ReferralStatuses",
                column: "ReferralStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExaminationClaims_ClaimStatusId",
                table: "UserExaminationClaims",
                column: "ClaimStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExaminationClaims_ExaminationId",
                table: "UserExaminationClaims",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExaminationClaims_UserId",
                table: "UserExaminationClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExaminationNotifications_NotificationTextId",
                table: "UserExaminationNotifications",
                column: "NotificationTextId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExaminationNotifications_UserId",
                table: "UserExaminationNotifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganisationId",
                table: "Users",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileTypeId",
                table: "Users",
                column: "ProfileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Section12ApprovalStatusId",
                table: "Users",
                column: "Section12ApprovalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSpecialities_SpecialityId",
                table: "UserSpecialities",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSpecialities_UserId",
                table: "UserSpecialities",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankDetails");

            migrationBuilder.DropTable(
                name: "BankDetailsAudit");

            migrationBuilder.DropTable(
                name: "ClaimStatusAudits");

            migrationBuilder.DropTable(
                name: "ContactDetailAudits");

            migrationBuilder.DropTable(
                name: "ContactDetails");

            migrationBuilder.DropTable(
                name: "ContactDetailTypeAudits");

            migrationBuilder.DropTable(
                name: "DoctorStatusAudits");

            migrationBuilder.DropTable(
                name: "DoctorStatuses");

            migrationBuilder.DropTable(
                name: "ExaminationAudits");

            migrationBuilder.DropTable(
                name: "GpPracticeAudits");

            migrationBuilder.DropTable(
                name: "NonPaymentLocationAudits");

            migrationBuilder.DropTable(
                name: "NonPaymentLocationTypeAudits");

            migrationBuilder.DropTable(
                name: "NotificationTextAudits");

            migrationBuilder.DropTable(
                name: "OnCallUserAudits");

            migrationBuilder.DropTable(
                name: "OnCallUsers");

            migrationBuilder.DropTable(
                name: "OrganisationAudits");

            migrationBuilder.DropTable(
                name: "PatientAudits");

            migrationBuilder.DropTable(
                name: "PaymentMethodAudits");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "PaymentMethodTypeAudits");

            migrationBuilder.DropTable(
                name: "PaymentRuleAudits");

            migrationBuilder.DropTable(
                name: "PaymentRules");

            migrationBuilder.DropTable(
                name: "PaymentRuleSetAudits");

            migrationBuilder.DropTable(
                name: "ProfileTypeAudits");

            migrationBuilder.DropTable(
                name: "ReferralAudits");

            migrationBuilder.DropTable(
                name: "ReferralStatusAudits");

            migrationBuilder.DropTable(
                name: "Section12ApprovalStatusAudits");

            migrationBuilder.DropTable(
                name: "SpecialityAudits");

            migrationBuilder.DropTable(
                name: "UnsuccessfulExaminationTypeAudits");

            migrationBuilder.DropTable(
                name: "UserAudits");

            migrationBuilder.DropTable(
                name: "UserExaminationClaimAudits");

            migrationBuilder.DropTable(
                name: "UserExaminationClaims");

            migrationBuilder.DropTable(
                name: "UserExaminationNotificationAudits");

            migrationBuilder.DropTable(
                name: "UserExaminationNotifications");

            migrationBuilder.DropTable(
                name: "UserSpecialitieAudits");

            migrationBuilder.DropTable(
                name: "UserSpecialities");

            migrationBuilder.DropTable(
                name: "ContactDetailTypes");

            migrationBuilder.DropTable(
                name: "CcgAudits");

            migrationBuilder.DropTable(
                name: "PaymentMethodTypes");

            migrationBuilder.DropTable(
                name: "PaymentRuleSets");

            migrationBuilder.DropTable(
                name: "ClaimStatuses");

            migrationBuilder.DropTable(
                name: "Examinations");

            migrationBuilder.DropTable(
                name: "NotificationTexts");

            migrationBuilder.DropTable(
                name: "NonPaymentLocations");

            migrationBuilder.DropTable(
                name: "Referrals");

            migrationBuilder.DropTable(
                name: "Specialities");

            migrationBuilder.DropTable(
                name: "UnsuccessfulExaminationTypes");

            migrationBuilder.DropTable(
                name: "NonPaymentLocationTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "ReferralStatuses");

            migrationBuilder.DropTable(
                name: "Organisations");

            migrationBuilder.DropTable(
                name: "ProfileTypes");

            migrationBuilder.DropTable(
                name: "Section12ApprovalStatuses");

            migrationBuilder.DropTable(
                name: "GpPractices");

            migrationBuilder.DropTable(
                name: "Ccgs");
        }
    }
}
