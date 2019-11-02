using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fmas12d.Business.Migrations
{
    public partial class InitialAfterExaminationNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssessmentDetailsAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    AssessmentId = table.Column<int>(nullable: false),
                    AssessmentDetailTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentDetailsAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentDetailTypesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentDetailTypesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentDoctorsAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    AttendanceConfirmedByUserId = table.Column<int>(nullable: true),
                    DoctorUserId = table.Column<int>(nullable: false),
                    AssessmentId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentDoctorsAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentDoctorStatusesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentDoctorStatusesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentsAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Address1 = table.Column<string>(maxLength: 200, nullable: false),
                    Address2 = table.Column<string>(maxLength: 500, nullable: true),
                    Address3 = table.Column<string>(maxLength: 500, nullable: true),
                    Address4 = table.Column<string>(maxLength: 500, nullable: true),
                    CcgId = table.Column<int>(nullable: true),
                    CompletedByUserId = table.Column<int>(nullable: true),
                    CompletedTime = table.Column<DateTimeOffset>(nullable: true),
                    CompletionConfirmationByUserId = table.Column<int>(nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    IsSuccessful = table.Column<bool>(nullable: true),
                    MeetingArrangementComment = table.Column<string>(maxLength: 2000, nullable: true),
                    MustBeCompletedBy = table.Column<DateTimeOffset>(nullable: true),
                    NonPaymentLocationId = table.Column<int>(nullable: true),
                    Postcode = table.Column<string>(maxLength: 10, nullable: false),
                    PreferredDoctorGenderTypeId = table.Column<int>(nullable: true),
                    ReferralId = table.Column<int>(nullable: false),
                    ScheduledTime = table.Column<DateTimeOffset>(nullable: true),
                    SpecialityId = table.Column<int>(nullable: true),
                    UnsuccessfulAssessmentTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentsAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "BankDetailsAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
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
                    table.PrimaryKey("PK_BankDetailsAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "CcgsAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    CostCentre = table.Column<int>(nullable: false),
                    FailedAssessmentPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPaymentApprovalRequired = table.Column<bool>(nullable: false),
                    LongCode = table.Column<string>(maxLength: 10, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    ShortCode = table.Column<string>(maxLength: 5, nullable: true),
                    SuccessfulPencePerMile = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnsuccessfulPencePerMile = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CcgsAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "ClaimStatusesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimStatusesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetailsAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Address1 = table.Column<string>(maxLength: 200, nullable: false),
                    Address2 = table.Column<string>(maxLength: 200, nullable: true),
                    Address3 = table.Column<string>(maxLength: 200, nullable: true),
                    CcgId = table.Column<int>(nullable: false),
                    ContactDetailTypeId = table.Column<int>(nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 100, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(8,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    Postcode = table.Column<string>(maxLength: 10, nullable: true),
                    TelephoneNumber = table.Column<int>(nullable: true),
                    Town = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetailsAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetailTypesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetailTypesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "DoctorStatusesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    AvailabilityEnd = table.Column<DateTimeOffset>(nullable: false),
                    AvailabilityStart = table.Column<DateTimeOffset>(nullable: false),
                    ExtendedAvailabilityEnd1 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityEnd2 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityEnd3 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityLatitude1 = table.Column<decimal>(type: "decimal(8,6)", nullable: true),
                    ExtendedAvailabilityLatitude2 = table.Column<decimal>(type: "decimal(8,6)", nullable: true),
                    ExtendedAvailabilityLatitude3 = table.Column<decimal>(type: "decimal(8,6)", nullable: true),
                    ExtendedAvailabilityLongitude1 = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    ExtendedAvailabilityLongitude2 = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    ExtendedAvailabilityLongitude3 = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    ExtendedAvailabilityStart1 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityStart2 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityStart3 = table.Column<DateTimeOffset>(nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(8,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorStatusesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "GenderTypesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderTypesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "GpPracticesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Postcode = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpPracticesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(nullable: true),
                    MessageTemplate = table.Column<string>(nullable: true),
                    Level = table.Column<string>(maxLength: 128, nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Exception = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NonPaymentLocationsAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    NonPaymentLocationTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonPaymentLocationsAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "NonPaymentLocationTypesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonPaymentLocationTypesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTextsAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    MessageTemplate = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTextsAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "OnCallUsersAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    DateTimeEnd = table.Column<DateTimeOffset>(nullable: false),
                    DateTimeStart = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnCallUsersAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "OrganisationsAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationsAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "PatientsAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    AlternativeIdentifier = table.Column<string>(maxLength: 200, nullable: true),
                    CcgId = table.Column<int>(nullable: true),
                    GpPracticeId = table.Column<int>(nullable: true),
                    NhsNumber = table.Column<long>(nullable: true),
                    ResidentialPostcode = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientsAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethodsAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    PaymentMethodTypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethodsAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethodTypesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethodTypesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentRulesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Criteria = table.Column<string>(maxLength: 2000, nullable: false),
                    PaymentRuleSetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRulesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentRuleSetsAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    DateTimeFrom = table.Column<DateTimeOffset>(nullable: false),
                    DateTimeTo = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRuleSetsAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "ProfileTypesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileTypesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "ReferralsAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    IsPlannedAssessment = table.Column<bool>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    ReferralStatusId = table.Column<int>(nullable: false),
                    LeadAmhpUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferralsAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "ReferralStatusesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferralStatusesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "Section12ApprovalStatusesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section12ApprovalStatusesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "SpecialitiesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    FinanceMileageSubjectiveCode = table.Column<int>(nullable: true),
                    FinanceSubjectiveCode = table.Column<int>(nullable: true),
                    LevelOfUrgencyTimescaleMinutes = table.Column<int>(nullable: false),
                    NonSection12Payment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Section12Payment = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialitiesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "UnsuccessfulAssessmentTypesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnsuccessfulAssessmentTypesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "UserAssessmentClaimsAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    ClaimReference = table.Column<int>(nullable: true),
                    ClaimStatusId = table.Column<int>(nullable: true),
                    AssessmentId = table.Column<int>(nullable: false),
                    AssessmentPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsAttendanceConfirmed = table.Column<bool>(nullable: false),
                    IsClaimable = table.Column<bool>(nullable: true),
                    Mileage = table.Column<int>(nullable: true),
                    MileagePayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentDate = table.Column<DateTimeOffset>(nullable: true),
                    SelectedByUserId = table.Column<int>(nullable: false),
                    StartPostcode = table.Column<string>(maxLength: 10, nullable: false),
                    TravelComments = table.Column<string>(maxLength: 2000, nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    HasBeenDeallocated = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssessmentClaimsAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "UserAssessmentNotificationsAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    AssessmentId = table.Column<int>(nullable: false),
                    HasAccepted = table.Column<bool>(nullable: true),
                    NotificationTextId = table.Column<int>(nullable: false),
                    RespondedAt = table.Column<DateTimeOffset>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssessmentNotificationsAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "UsersAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    DisplayName = table.Column<string>(maxLength: 256, nullable: true),
                    GenderTypeId = table.Column<int>(nullable: true),
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
                    table.PrimaryKey("PK_UsersAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "UserSpecialitiesAudit",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditAction = table.Column<string>(nullable: true),
                    AuditDuration = table.Column<int>(nullable: false),
                    AuditErrorMessage = table.Column<string>(nullable: true),
                    AuditResult = table.Column<int>(nullable: false),
                    AuditSuccess = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    SpecialityId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSpecialitiesAudit", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    AssessmentId = table.Column<int>(nullable: false),
                    AssessmentDetailTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentDetails", x => x.Id);
                    table.UniqueConstraint("AK_AssessmentDetails_AssessmentDetailTypeId_AssessmentId", x => new { x.AssessmentDetailTypeId, x.AssessmentId });
                });

            migrationBuilder.CreateTable(
                name: "AssessmentDoctors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    AttendanceConfirmedByUserId = table.Column<int>(nullable: true),
                    DoctorUserId = table.Column<int>(nullable: false),
                    AssessmentId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentDoctors", x => x.Id);
                    table.UniqueConstraint("AK_AssessmentDoctors_AssessmentId_DoctorUserId", x => new { x.AssessmentId, x.DoctorUserId });
                });

            migrationBuilder.CreateTable(
                name: "UserAssessmentClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    ClaimReference = table.Column<int>(nullable: true),
                    ClaimStatusId = table.Column<int>(nullable: true),
                    AssessmentId = table.Column<int>(nullable: false),
                    AssessmentPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsAttendanceConfirmed = table.Column<bool>(nullable: false),
                    IsClaimable = table.Column<bool>(nullable: true),
                    Mileage = table.Column<int>(nullable: true),
                    MileagePayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentDate = table.Column<DateTimeOffset>(nullable: true),
                    SelectedByUserId = table.Column<int>(nullable: false),
                    StartPostcode = table.Column<string>(maxLength: 10, nullable: false),
                    TravelComments = table.Column<string>(maxLength: 2000, nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    HasBeenDeallocated = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssessmentClaims", x => x.Id);
                    table.UniqueConstraint("AK_UserAssessmentClaims_AssessmentId_UserId", x => new { x.AssessmentId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "UserAssessmentNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    AssessmentId = table.Column<int>(nullable: false),
                    HasAccepted = table.Column<bool>(nullable: true),
                    NotificationTextId = table.Column<int>(nullable: false),
                    RespondedAt = table.Column<DateTimeOffset>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssessmentNotifications", x => x.Id);
                    table.UniqueConstraint("AK_UserAssessmentNotifications_AssessmentId_NotificationTextId_UserId", x => new { x.AssessmentId, x.NotificationTextId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Address1 = table.Column<string>(maxLength: 200, nullable: false),
                    Address2 = table.Column<string>(maxLength: 500, nullable: true),
                    Address3 = table.Column<string>(maxLength: 500, nullable: true),
                    Address4 = table.Column<string>(maxLength: 500, nullable: true),
                    AmhpUserId = table.Column<int>(nullable: false),
                    CcgId = table.Column<int>(nullable: true),
                    CompletedByUserId = table.Column<int>(nullable: true),
                    CompletedTime = table.Column<DateTimeOffset>(nullable: true),
                    CompletionConfirmationByUserId = table.Column<int>(nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    IsSuccessful = table.Column<bool>(nullable: true),
                    MeetingArrangementComment = table.Column<string>(maxLength: 2000, nullable: true),
                    MustBeCompletedBy = table.Column<DateTimeOffset>(nullable: true),
                    NonPaymentLocationId = table.Column<int>(nullable: true),
                    Postcode = table.Column<string>(maxLength: 10, nullable: false),
                    PreferredDoctorGenderTypeId = table.Column<int>(nullable: true),
                    ReferralId = table.Column<int>(nullable: false),
                    ScheduledTime = table.Column<DateTimeOffset>(nullable: true),
                    SpecialityId = table.Column<int>(nullable: true),
                    UnsuccessfulAssessmentTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Address1 = table.Column<string>(maxLength: 200, nullable: false),
                    Address2 = table.Column<string>(maxLength: 200, nullable: true),
                    Address3 = table.Column<string>(maxLength: 200, nullable: true),
                    CcgId = table.Column<int>(nullable: false),
                    ContactDetailTypeId = table.Column<int>(nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 100, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(8,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    Postcode = table.Column<string>(maxLength: 10, nullable: true),
                    TelephoneNumber = table.Column<int>(nullable: true),
                    Town = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetails", x => x.Id);
                    table.UniqueConstraint("AK_ContactDetails_CcgId_ContactDetailTypeId_UserId", x => new { x.CcgId, x.ContactDetailTypeId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "GpPractices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Postcode = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpPractices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NonPaymentLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    NonPaymentLocationTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonPaymentLocations", x => x.Id);
                    table.UniqueConstraint("AK_NonPaymentLocations_CcgId_NonPaymentLocationTypeId", x => new { x.CcgId, x.NonPaymentLocationTypeId });
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    AlternativeIdentifier = table.Column<string>(maxLength: 200, nullable: true),
                    CcgId = table.Column<int>(nullable: true),
                    GpPracticeId = table.Column<int>(nullable: true),
                    NhsNumber = table.Column<long>(nullable: true),
                    ResidentialPostcode = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_GpPractices_GpPracticeId",
                        column: x => x.GpPracticeId,
                        principalTable: "GpPractices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    PaymentMethodTypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                    table.UniqueConstraint("AK_PaymentMethods_CcgId_PaymentMethodTypeId_UserId", x => new { x.CcgId, x.PaymentMethodTypeId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "PaymentRuleSets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    CcgId = table.Column<int>(nullable: false),
                    DateTimeFrom = table.Column<DateTimeOffset>(nullable: false),
                    DateTimeTo = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRuleSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    DisplayName = table.Column<string>(maxLength: 256, nullable: true),
                    GenderTypeId = table.Column<int>(nullable: true),
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
                        name: "FK_Users_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentDetailTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentDetailTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentDetailTypes_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentDoctorStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentDoctorStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentDoctorStatuses_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ccgs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    CostCentre = table.Column<int>(nullable: false),
                    FailedAssessmentPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPaymentApprovalRequired = table.Column<bool>(nullable: false),
                    LongCode = table.Column<string>(maxLength: 10, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    ShortCode = table.Column<string>(maxLength: 5, nullable: true),
                    SuccessfulPencePerMile = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnsuccessfulPencePerMile = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ccgs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ccgs_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClaimStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaimStatuses_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetailTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetailTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactDetailTypes_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    AvailabilityEnd = table.Column<DateTimeOffset>(nullable: false),
                    AvailabilityStart = table.Column<DateTimeOffset>(nullable: false),
                    ExtendedAvailabilityEnd1 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityEnd2 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityEnd3 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityLatitude1 = table.Column<decimal>(type: "decimal(8,6)", nullable: true),
                    ExtendedAvailabilityLatitude2 = table.Column<decimal>(type: "decimal(8,6)", nullable: true),
                    ExtendedAvailabilityLatitude3 = table.Column<decimal>(type: "decimal(8,6)", nullable: true),
                    ExtendedAvailabilityLongitude1 = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    ExtendedAvailabilityLongitude2 = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    ExtendedAvailabilityLongitude3 = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    ExtendedAvailabilityStart1 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityStart2 = table.Column<DateTimeOffset>(nullable: true),
                    ExtendedAvailabilityStart3 = table.Column<DateTimeOffset>(nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(8,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorStatuses_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoctorStatuses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GenderTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenderTypes_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NonPaymentLocationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonPaymentLocationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NonPaymentLocationTypes_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTexts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    MessageTemplate = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTexts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationTexts_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OnCallUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    DateTimeEnd = table.Column<DateTimeOffset>(nullable: false),
                    DateTimeStart = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnCallUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnCallUsers_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OnCallUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organisations_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethodTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethodTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentMethodTypes_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentRules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Criteria = table.Column<string>(maxLength: 2000, nullable: false),
                    PaymentRuleSetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentRules_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentRules_PaymentRuleSets_PaymentRuleSetId",
                        column: x => x.PaymentRuleSetId,
                        principalTable: "PaymentRuleSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileTypes_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReferralStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferralStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferralStatuses_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Section12ApprovalStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section12ApprovalStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Section12ApprovalStatuses_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Specialities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    FinanceMileageSubjectiveCode = table.Column<int>(nullable: true),
                    FinanceSubjectiveCode = table.Column<int>(nullable: true),
                    LevelOfUrgencyTimescaleMinutes = table.Column<int>(nullable: false),
                    NonSection12Payment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Section12Payment = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specialities_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnsuccessfulAssessmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnsuccessfulAssessmentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnsuccessfulAssessmentTypes_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Referrals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    IsPlannedAssessment = table.Column<bool>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    ReferralStatusId = table.Column<int>(nullable: false),
                    LeadAmhpUserId = table.Column<int>(nullable: false)
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
                        name: "FK_Referrals_Users_LeadAmhpUserId",
                        column: x => x.LeadAmhpUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Referrals_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
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
                name: "UserSpecialities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: false),
                    SpecialityId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSpecialities", x => x.Id);
                    table.UniqueConstraint("AK_UserSpecialities_SpecialityId_UserId", x => new { x.SpecialityId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserSpecialities_Users_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentDetails_AssessmentId",
                table: "AssessmentDetails",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentDetails_ModifiedByUserId",
                table: "AssessmentDetails",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentDetailTypes_ModifiedByUserId",
                table: "AssessmentDetailTypes",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentDetailTypes_Name",
                table: "AssessmentDetailTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentDoctors_AttendanceConfirmedByUserId",
                table: "AssessmentDoctors",
                column: "AttendanceConfirmedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentDoctors_DoctorUserId",
                table: "AssessmentDoctors",
                column: "DoctorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentDoctors_ModifiedByUserId",
                table: "AssessmentDoctors",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentDoctors_StatusId",
                table: "AssessmentDoctors",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentDoctorStatuses_ModifiedByUserId",
                table: "AssessmentDoctorStatuses",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentDoctorStatuses_Name",
                table: "AssessmentDoctorStatuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_AmhpUserId",
                table: "Assessments",
                column: "AmhpUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_CcgId",
                table: "Assessments",
                column: "CcgId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_CompletedByUserId",
                table: "Assessments",
                column: "CompletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_CompletionConfirmationByUserId",
                table: "Assessments",
                column: "CompletionConfirmationByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_CreatedByUserId",
                table: "Assessments",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_ModifiedByUserId",
                table: "Assessments",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_NonPaymentLocationId",
                table: "Assessments",
                column: "NonPaymentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_PreferredDoctorGenderTypeId",
                table: "Assessments",
                column: "PreferredDoctorGenderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_ReferralId",
                table: "Assessments",
                column: "ReferralId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_SpecialityId",
                table: "Assessments",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_UnsuccessfulAssessmentTypeId",
                table: "Assessments",
                column: "UnsuccessfulAssessmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BankDetails_ModifiedByUserId",
                table: "BankDetails",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BankDetails_UserId",
                table: "BankDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ccgs_LongCode",
                table: "Ccgs",
                column: "LongCode",
                unique: true,
                filter: "[LongCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ccgs_ModifiedByUserId",
                table: "Ccgs",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ccgs_Name",
                table: "Ccgs",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ccgs_ShortCode",
                table: "Ccgs",
                column: "ShortCode",
                unique: true,
                filter: "[ShortCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimStatuses_ModifiedByUserId",
                table: "ClaimStatuses",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimStatuses_Name",
                table: "ClaimStatuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_ContactDetailTypeId",
                table: "ContactDetails",
                column: "ContactDetailTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_ModifiedByUserId",
                table: "ContactDetails",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_UserId",
                table: "ContactDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetailTypes_ModifiedByUserId",
                table: "ContactDetailTypes",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetailTypes_Name",
                table: "ContactDetailTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorStatuses_ModifiedByUserId",
                table: "DoctorStatuses",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorStatuses_UserId",
                table: "DoctorStatuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GenderTypes_ModifiedByUserId",
                table: "GenderTypes",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GenderTypes_Name",
                table: "GenderTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GpPractices_CcgId",
                table: "GpPractices",
                column: "CcgId");

            migrationBuilder.CreateIndex(
                name: "IX_GpPractices_Code",
                table: "GpPractices",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GpPractices_ModifiedByUserId",
                table: "GpPractices",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_NonPaymentLocations_ModifiedByUserId",
                table: "NonPaymentLocations",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_NonPaymentLocations_NonPaymentLocationTypeId",
                table: "NonPaymentLocations",
                column: "NonPaymentLocationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NonPaymentLocationTypes_ModifiedByUserId",
                table: "NonPaymentLocationTypes",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_NonPaymentLocationTypes_Name",
                table: "NonPaymentLocationTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTexts_ModifiedByUserId",
                table: "NotificationTexts",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTexts_Name",
                table: "NotificationTexts",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OnCallUsers_ModifiedByUserId",
                table: "OnCallUsers",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OnCallUsers_UserId",
                table: "OnCallUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_ModifiedByUserId",
                table: "Organisations",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_Name",
                table: "Organisations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AlternativeIdentifier",
                table: "Patients",
                column: "AlternativeIdentifier",
                unique: true,
                filter: "[AlternativeIdentifier] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_CcgId",
                table: "Patients",
                column: "CcgId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_GpPracticeId",
                table: "Patients",
                column: "GpPracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ModifiedByUserId",
                table: "Patients",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_NhsNumber",
                table: "Patients",
                column: "NhsNumber",
                unique: true,
                filter: "[NhsNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_ModifiedByUserId",
                table: "PaymentMethods",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_PaymentMethodTypeId",
                table: "PaymentMethods",
                column: "PaymentMethodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_UserId",
                table: "PaymentMethods",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethodTypes_ModifiedByUserId",
                table: "PaymentMethodTypes",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethodTypes_Name",
                table: "PaymentMethodTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRules_ModifiedByUserId",
                table: "PaymentRules",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRules_PaymentRuleSetId",
                table: "PaymentRules",
                column: "PaymentRuleSetId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRuleSets_CcgId",
                table: "PaymentRuleSets",
                column: "CcgId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRuleSets_ModifiedByUserId",
                table: "PaymentRuleSets",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileTypes_ModifiedByUserId",
                table: "ProfileTypes",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileTypes_Name",
                table: "ProfileTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Referrals_CreatedByUserId",
                table: "Referrals",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Referrals_LeadAmhpUserId",
                table: "Referrals",
                column: "LeadAmhpUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Referrals_ModifiedByUserId",
                table: "Referrals",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Referrals_PatientId",
                table: "Referrals",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Referrals_ReferralStatusId",
                table: "Referrals",
                column: "ReferralStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferralStatuses_ModifiedByUserId",
                table: "ReferralStatuses",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferralStatuses_Name",
                table: "ReferralStatuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Section12ApprovalStatuses_ModifiedByUserId",
                table: "Section12ApprovalStatuses",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Section12ApprovalStatuses_Name",
                table: "Section12ApprovalStatuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialities_ModifiedByUserId",
                table: "Specialities",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UnsuccessfulAssessmentTypes_ModifiedByUserId",
                table: "UnsuccessfulAssessmentTypes",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UnsuccessfulAssessmentTypes_Name",
                table: "UnsuccessfulAssessmentTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAssessmentClaims_ClaimStatusId",
                table: "UserAssessmentClaims",
                column: "ClaimStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssessmentClaims_ModifiedByUserId",
                table: "UserAssessmentClaims",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssessmentClaims_SelectedByUserId",
                table: "UserAssessmentClaims",
                column: "SelectedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssessmentClaims_UserId",
                table: "UserAssessmentClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssessmentNotifications_ModifiedByUserId",
                table: "UserAssessmentNotifications",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssessmentNotifications_NotificationTextId",
                table: "UserAssessmentNotifications",
                column: "NotificationTextId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssessmentNotifications_UserId",
                table: "UserAssessmentNotifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderTypeId",
                table: "Users",
                column: "GenderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GmcNumber",
                table: "Users",
                column: "GmcNumber",
                unique: true,
                filter: "[GmcNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdentityServerIdentifier",
                table: "Users",
                column: "IdentityServerIdentifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ModifiedByUserId",
                table: "Users",
                column: "ModifiedByUserId");

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
                name: "IX_UserSpecialities_ModifiedByUserId",
                table: "UserSpecialities",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSpecialities_UserId",
                table: "UserSpecialities",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentDetails_AssessmentDetailTypes_AssessmentDetailTypeId",
                table: "AssessmentDetails",
                column: "AssessmentDetailTypeId",
                principalTable: "AssessmentDetailTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentDetails_Assessments_AssessmentId",
                table: "AssessmentDetails",
                column: "AssessmentId",
                principalTable: "Assessments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentDetails_Users_ModifiedByUserId",
                table: "AssessmentDetails",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentDoctors_Assessments_AssessmentId",
                table: "AssessmentDoctors",
                column: "AssessmentId",
                principalTable: "Assessments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentDoctors_Users_AttendanceConfirmedByUserId",
                table: "AssessmentDoctors",
                column: "AttendanceConfirmedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentDoctors_Users_DoctorUserId",
                table: "AssessmentDoctors",
                column: "DoctorUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentDoctors_Users_ModifiedByUserId",
                table: "AssessmentDoctors",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentDoctors_AssessmentDoctorStatuses_StatusId",
                table: "AssessmentDoctors",
                column: "StatusId",
                principalTable: "AssessmentDoctorStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssessmentClaims_Assessments_AssessmentId",
                table: "UserAssessmentClaims",
                column: "AssessmentId",
                principalTable: "Assessments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssessmentClaims_Users_ModifiedByUserId",
                table: "UserAssessmentClaims",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssessmentClaims_Users_SelectedByUserId",
                table: "UserAssessmentClaims",
                column: "SelectedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssessmentClaims_Users_UserId",
                table: "UserAssessmentClaims",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssessmentClaims_ClaimStatuses_ClaimStatusId",
                table: "UserAssessmentClaims",
                column: "ClaimStatusId",
                principalTable: "ClaimStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssessmentNotifications_Assessments_AssessmentId",
                table: "UserAssessmentNotifications",
                column: "AssessmentId",
                principalTable: "Assessments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssessmentNotifications_Users_ModifiedByUserId",
                table: "UserAssessmentNotifications",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssessmentNotifications_Users_UserId",
                table: "UserAssessmentNotifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssessmentNotifications_NotificationTexts_NotificationTextId",
                table: "UserAssessmentNotifications",
                column: "NotificationTextId",
                principalTable: "NotificationTexts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Users_AmhpUserId",
                table: "Assessments",
                column: "AmhpUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Users_CompletedByUserId",
                table: "Assessments",
                column: "CompletedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Users_CompletionConfirmationByUserId",
                table: "Assessments",
                column: "CompletionConfirmationByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Users_CreatedByUserId",
                table: "Assessments",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Users_ModifiedByUserId",
                table: "Assessments",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Ccgs_CcgId",
                table: "Assessments",
                column: "CcgId",
                principalTable: "Ccgs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_NonPaymentLocations_NonPaymentLocationId",
                table: "Assessments",
                column: "NonPaymentLocationId",
                principalTable: "NonPaymentLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_GenderTypes_PreferredDoctorGenderTypeId",
                table: "Assessments",
                column: "PreferredDoctorGenderTypeId",
                principalTable: "GenderTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Referrals_ReferralId",
                table: "Assessments",
                column: "ReferralId",
                principalTable: "Referrals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Specialities_SpecialityId",
                table: "Assessments",
                column: "SpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_UnsuccessfulAssessmentTypes_UnsuccessfulAssessmentTypeId",
                table: "Assessments",
                column: "UnsuccessfulAssessmentTypeId",
                principalTable: "UnsuccessfulAssessmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankDetails_Users_ModifiedByUserId",
                table: "BankDetails",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankDetails_Users_UserId",
                table: "BankDetails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankDetails_Ccgs_CcgId",
                table: "BankDetails",
                column: "CcgId",
                principalTable: "Ccgs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDetails_Users_ModifiedByUserId",
                table: "ContactDetails",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDetails_Users_UserId",
                table: "ContactDetails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDetails_Ccgs_CcgId",
                table: "ContactDetails",
                column: "CcgId",
                principalTable: "Ccgs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDetails_ContactDetailTypes_ContactDetailTypeId",
                table: "ContactDetails",
                column: "ContactDetailTypeId",
                principalTable: "ContactDetailTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GpPractices_Users_ModifiedByUserId",
                table: "GpPractices",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GpPractices_Ccgs_CcgId",
                table: "GpPractices",
                column: "CcgId",
                principalTable: "Ccgs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NonPaymentLocations_Users_ModifiedByUserId",
                table: "NonPaymentLocations",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NonPaymentLocations_Ccgs_CcgId",
                table: "NonPaymentLocations",
                column: "CcgId",
                principalTable: "Ccgs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NonPaymentLocations_NonPaymentLocationTypes_NonPaymentLocationTypeId",
                table: "NonPaymentLocations",
                column: "NonPaymentLocationTypeId",
                principalTable: "NonPaymentLocationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Users_ModifiedByUserId",
                table: "Patients",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Ccgs_CcgId",
                table: "Patients",
                column: "CcgId",
                principalTable: "Ccgs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_Users_ModifiedByUserId",
                table: "PaymentMethods",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_Users_UserId",
                table: "PaymentMethods",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_Ccgs_CcgId",
                table: "PaymentMethods",
                column: "CcgId",
                principalTable: "Ccgs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_PaymentMethodTypes_PaymentMethodTypeId",
                table: "PaymentMethods",
                column: "PaymentMethodTypeId",
                principalTable: "PaymentMethodTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRuleSets_Users_ModifiedByUserId",
                table: "PaymentRuleSets",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRuleSets_Ccgs_CcgId",
                table: "PaymentRuleSets",
                column: "CcgId",
                principalTable: "Ccgs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_GenderTypes_GenderTypeId",
                table: "Users",
                column: "GenderTypeId",
                principalTable: "GenderTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Organisations_OrganisationId",
                table: "Users",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ProfileTypes_ProfileTypeId",
                table: "Users",
                column: "ProfileTypeId",
                principalTable: "ProfileTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Section12ApprovalStatuses_Section12ApprovalStatusId",
                table: "Users",
                column: "Section12ApprovalStatusId",
                principalTable: "Section12ApprovalStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            InitialSystemUserSeed.Seed(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenderTypes_Users_ModifiedByUserId",
                table: "GenderTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Organisations_Users_ModifiedByUserId",
                table: "Organisations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileTypes_Users_ModifiedByUserId",
                table: "ProfileTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Section12ApprovalStatuses_Users_ModifiedByUserId",
                table: "Section12ApprovalStatuses");

            migrationBuilder.DropTable(
                name: "AssessmentDetails");

            migrationBuilder.DropTable(
                name: "AssessmentDetailsAudit");

            migrationBuilder.DropTable(
                name: "AssessmentDetailTypesAudit");

            migrationBuilder.DropTable(
                name: "AssessmentDoctors");

            migrationBuilder.DropTable(
                name: "AssessmentDoctorsAudit");

            migrationBuilder.DropTable(
                name: "AssessmentDoctorStatusesAudit");

            migrationBuilder.DropTable(
                name: "AssessmentsAudit");

            migrationBuilder.DropTable(
                name: "BankDetails");

            migrationBuilder.DropTable(
                name: "BankDetailsAudit");

            migrationBuilder.DropTable(
                name: "CcgsAudit");

            migrationBuilder.DropTable(
                name: "ClaimStatusesAudit");

            migrationBuilder.DropTable(
                name: "ContactDetails");

            migrationBuilder.DropTable(
                name: "ContactDetailsAudit");

            migrationBuilder.DropTable(
                name: "ContactDetailTypesAudit");

            migrationBuilder.DropTable(
                name: "DoctorStatuses");

            migrationBuilder.DropTable(
                name: "DoctorStatusesAudit");

            migrationBuilder.DropTable(
                name: "GenderTypesAudit");

            migrationBuilder.DropTable(
                name: "GpPracticesAudit");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "NonPaymentLocationsAudit");

            migrationBuilder.DropTable(
                name: "NonPaymentLocationTypesAudit");

            migrationBuilder.DropTable(
                name: "NotificationTextsAudit");

            migrationBuilder.DropTable(
                name: "OnCallUsers");

            migrationBuilder.DropTable(
                name: "OnCallUsersAudit");

            migrationBuilder.DropTable(
                name: "OrganisationsAudit");

            migrationBuilder.DropTable(
                name: "PatientsAudit");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "PaymentMethodsAudit");

            migrationBuilder.DropTable(
                name: "PaymentMethodTypesAudit");

            migrationBuilder.DropTable(
                name: "PaymentRules");

            migrationBuilder.DropTable(
                name: "PaymentRulesAudit");

            migrationBuilder.DropTable(
                name: "PaymentRuleSetsAudit");

            migrationBuilder.DropTable(
                name: "ProfileTypesAudit");

            migrationBuilder.DropTable(
                name: "ReferralsAudit");

            migrationBuilder.DropTable(
                name: "ReferralStatusesAudit");

            migrationBuilder.DropTable(
                name: "Section12ApprovalStatusesAudit");

            migrationBuilder.DropTable(
                name: "SpecialitiesAudit");

            migrationBuilder.DropTable(
                name: "UnsuccessfulAssessmentTypesAudit");

            migrationBuilder.DropTable(
                name: "UserAssessmentClaims");

            migrationBuilder.DropTable(
                name: "UserAssessmentClaimsAudit");

            migrationBuilder.DropTable(
                name: "UserAssessmentNotifications");

            migrationBuilder.DropTable(
                name: "UserAssessmentNotificationsAudit");

            migrationBuilder.DropTable(
                name: "UsersAudit");

            migrationBuilder.DropTable(
                name: "UserSpecialities");

            migrationBuilder.DropTable(
                name: "UserSpecialitiesAudit");

            migrationBuilder.DropTable(
                name: "AssessmentDetailTypes");

            migrationBuilder.DropTable(
                name: "AssessmentDoctorStatuses");

            migrationBuilder.DropTable(
                name: "ContactDetailTypes");

            migrationBuilder.DropTable(
                name: "PaymentMethodTypes");

            migrationBuilder.DropTable(
                name: "PaymentRuleSets");

            migrationBuilder.DropTable(
                name: "ClaimStatuses");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "NotificationTexts");

            migrationBuilder.DropTable(
                name: "NonPaymentLocations");

            migrationBuilder.DropTable(
                name: "Referrals");

            migrationBuilder.DropTable(
                name: "Specialities");

            migrationBuilder.DropTable(
                name: "UnsuccessfulAssessmentTypes");

            migrationBuilder.DropTable(
                name: "NonPaymentLocationTypes");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "ReferralStatuses");

            migrationBuilder.DropTable(
                name: "GpPractices");

            migrationBuilder.DropTable(
                name: "Ccgs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "GenderTypes");

            migrationBuilder.DropTable(
                name: "Organisations");

            migrationBuilder.DropTable(
                name: "ProfileTypes");

            migrationBuilder.DropTable(
                name: "Section12ApprovalStatuses");
        }
    }
}
