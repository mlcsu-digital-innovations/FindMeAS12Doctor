using System.Linq;
using Audit.Core;
using Audit.EntityFramework;
using Fmas12d.Data.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Add this to the initial migration to create the inital system records: 
/// InitialSystemUserSeed.Seed(migrationBuilder); 
///
/// To add a new migration
/// dotnet ef migrations add <migration-name> --project Fmas12d.Business --startup-project Fmas12d.Api
/// 
/// To update the database with any required migrations
/// dotnet ef database update --project Fmas12d.Api
/// 
/// To update the Azure database you need to create a script from the last migration
/// dotnet ef migrations script <from-migration-name> --project=Fmas12d.Api > update.sql
/// 
/// To run the seeds on the Azure database you need to first add the fmas12d-development-api user
/// to the db_owner role
/// sp_addrolemember @rolename='db_owner', @membername='fmas12d-development-api'
/// 
/// Then run the seed with the AzureDevelopment launch profile
/// dotnet run /seednogppractice --launch-profile AzureDevelopment
/// 
/// Then remove fmas12d-development-api user from the db_owner role
/// sp_droprolemember @rolename='db_owner', @membername='fmas12d-development-api'
/// 
/// </summary>
namespace Fmas12d.Business
{
  public partial class ApplicationContext : AuditDbContext
  {
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
      : base(options)
    {
      //Audit.NET https://github.com/thepirat000/Audit.NET/tree/master/src/Audit.EntityFramework
      Audit.EntityFramework.Configuration.Setup()
          .ForContext<ApplicationContext>(config => config
              .IncludeEntityObjects()
              .AuditEventType("{context}:{database}"))
              .UseOptOut();

      Audit.Core.Configuration.Setup()
          .UseEntityFramework(x => x
              .AuditTypeNameMapper(typeName => typeName + "Audit")
              .AuditEntityAction<IBaseAudit>((auditEvent, eventEntry, auditEntity) =>
              {
                EntityFrameworkEvent efEvent = auditEvent.GetEntityFrameworkEvent();

                auditEntity.AuditAction = eventEntry.Action;
                auditEntity.AuditDuration = auditEvent.Duration;
                auditEntity.AuditErrorMessage = efEvent.ErrorMessage;
                auditEntity.AuditResult = efEvent.Result;
                auditEntity.AuditSuccess = efEvent.Success;

                return true;
              }));
    }

    public virtual DbSet<BankDetail> BankDetails { get; set; }
    public virtual DbSet<BankDetailAudit> BankDetailsAudit { get; set; }
    public virtual DbSet<Ccg> Ccgs { get; set; }
    public virtual DbSet<CcgAudit> CcgAudits { get; set; }
    public virtual DbSet<ClaimStatus> ClaimStatuses { get; set; }
    public virtual DbSet<ClaimStatusAudit> ClaimStatusAudits { get; set; }
    public virtual DbSet<ContactDetail> ContactDetails { get; set; }
    public virtual DbSet<ContactDetailAudit> ContactDetailAudits { get; set; }
    public virtual DbSet<ContactDetailCcg> ContactDetailCcgs { get; set; }
    public virtual DbSet<ContactDetailCcgAudit> ContactDetailCcgAudits { get; set; }
    public virtual DbSet<ContactDetailType> ContactDetailTypes { get; set; }
    public virtual DbSet<ContactDetailTypeAudit> ContactDetailTypeAudits { get; set; }
    public virtual DbSet<UserAvailability> UserAvailabilities { get; set; }
    public virtual DbSet<UserAvailabilityAudit> UserAvailabilityAudits { get; set; }
    public virtual DbSet<UserAvailabilityStatus> UserAvailabilityStatus { get; set; }
    public virtual DbSet<UserAvailabilityStatusAudit> UserAvailabilityStatusAudits { get; set; }
    public virtual DbSet<Assessment> Assessments { get; set; }
    public virtual DbSet<AssessmentAudit> AssessmentAudits { get; set; }
    public virtual DbSet<AssessmentDetail> AssessmentDetails { get; set; }
    public virtual DbSet<AssessmentDoctor> AssessmentDoctors { get; set; }
    public virtual DbSet<AssessmentDoctorAudit> AssessmentDoctorAudits { get; set; }
    public virtual DbSet<AssessmentDoctorStatus> AssessmentDoctorStatuses { get; set; }
    public virtual DbSet<AssessmentDoctorStatusAudit> AssessmentDoctorStatusAudits { get; set; }
    public virtual DbSet<AssessmentDetailAudit> AssessmentDetailAudits { get; set; }
    public virtual DbSet<AssessmentDetailType> AssessmentDetailTypes { get; set; }
    public virtual DbSet<AssessmentDetailTypeAudit> AssessmentDetailTypeAudits { get; set; }
    public virtual DbSet<GenderType> GenderTypes { get; set; }
    public virtual DbSet<GenderTypeAudit> GenderTypeAudits { get; set; }
    public virtual DbSet<GpPractice> GpPractices { get; set; }
    public virtual DbSet<GpPracticeAudit> GpPracticeAudits { get; set; }
    public virtual DbSet<Log> Logs { get; set; }
    public virtual DbSet<NonPaymentLocation> NonPaymentLocations { get; set; }
    public virtual DbSet<NonPaymentLocationAudit> NonPaymentLocationAudits { get; set; }
    public virtual DbSet<NonPaymentLocationType> NonPaymentLocationTypes { get; set; }
    public virtual DbSet<NonPaymentLocationTypeAudit> NonPaymentLocationTypeAudits { get; set; }
    public virtual DbSet<NotificationText> NotificationTexts { get; set; }
    public virtual DbSet<NotificationTextAudit> NotificationTextAudits { get; set; }
    public virtual DbSet<OnCallUser> OnCallUsers { get; set; }
    public virtual DbSet<OnCallUserAudit> OnCallUserAudits { get; set; }
    public virtual DbSet<Organisation> Organisations { get; set; }
    public virtual DbSet<OrganisationAudit> OrganisationAudits { get; set; }
    public virtual DbSet<Patient> Patients { get; set; }
    public virtual DbSet<PatientAudit> PatientAudits { get; set; }
    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
    public virtual DbSet<PaymentMethodAudit> PaymentMethodAudits { get; set; }
    public virtual DbSet<PaymentMethodType> PaymentMethodTypes { get; set; }
    public virtual DbSet<PaymentMethodTypeAudit> PaymentMethodTypeAudits { get; set; }
    public virtual DbSet<PaymentRule> PaymentRules { get; set; }
    public virtual DbSet<PaymentRuleAudit> PaymentRuleAudits { get; set; }
    public virtual DbSet<PaymentRuleSet> PaymentRuleSets { get; set; }
    public virtual DbSet<PaymentRuleSetAudit> PaymentRuleSetAudits { get; set; }
    public virtual DbSet<ProfileType> ProfileTypes { get; set; }
    public virtual DbSet<ProfileTypeAudit> ProfileTypeAudits { get; set; }
    public virtual DbSet<Referral> Referrals { get; set; }
    public virtual DbSet<ReferralAudit> ReferralAudits { get; set; }
    public virtual DbSet<ReferralStatus> ReferralStatuses { get; set; }
    public virtual DbSet<ReferralStatusAudit> ReferralStatusAudits { get; set; }
    public virtual DbSet<Section12ApprovalStatus> Section12ApprovalStatuses { get; set; }
    public virtual DbSet<Section12ApprovalStatusAudit> Section12ApprovalStatusAudits { get; set; }
    public virtual DbSet<Section12LiveRegister> Section12LiveRegisters { get; set; }
    public virtual DbSet<Section12LiveRegisterAudit> Section12LiveRegisterAudits { get; set; }    
    public virtual DbSet<Speciality> Specialities { get; set; }
    public virtual DbSet<SpecialityAudit> SpecialityAudits { get; set; }
    public virtual DbSet<UnsuccessfulAssessmentType> UnsuccessfulAssessmentTypes { get; set; }
    public virtual DbSet<UnsuccessfulAssessmentTypeAudit> UnsuccessfulAssessmentTypeAudits 
      { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserAudit> UserAudits { get; set; }
    public virtual DbSet<UserAssessmentClaim> UserAssessmentClaims { get; set; }
    public virtual DbSet<UserAssessmentClaimAudit> UserAssessmentClaimAudits { get; set; }
    public virtual DbSet<UserAssessmentNotification> UserAssessmentNotifications { get; set; }
    public virtual DbSet<UserAssessmentNotificationAudit> UserAssessmentNotificationAudits 
      { get; set; }
    public virtual DbSet<UserSpeciality> UserSpecialities { get; set; }
    public virtual DbSet<UserSpecialityAudit> UserSpecialitieAudits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

      base.OnModelCreating(modelBuilder);

      foreach (var entityType in modelBuilder.Model.GetEntityTypes())
      {
        entityType.GetForeignKeys()
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
            .ToList()
            .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
      }

      ConfigureManyToManyRelationships(modelBuilder);

      ConfigureUniqueIndexes(modelBuilder);
    }

    private void ConfigureUniqueIndexes(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Ccg>()
        .HasIndex(c => c.Name)
        .IsUnique();
      modelBuilder.Entity<Ccg>()
        .HasIndex(c => c.LongCode)
        .IsUnique();
      modelBuilder.Entity<Ccg>()
        .HasIndex(c => c.ShortCode)
        .IsUnique();

      modelBuilder.Entity<ClaimStatus>()
        .HasIndex(c => c.Name)
        .IsUnique();

      modelBuilder.Entity<ContactDetailType>()
        .HasIndex(c => c.Name)
        .IsUnique();

      modelBuilder.Entity<AssessmentDoctorStatus>()
        .HasIndex(c => c.Name)
        .IsUnique();

      modelBuilder.Entity<AssessmentDetailType>()
        .HasIndex(c => c.Name)
        .IsUnique();

      modelBuilder.Entity<GenderType>()
        .HasIndex(g => g.Name)
        .IsUnique();

      modelBuilder.Entity<GpPractice>()
        .HasIndex(g => g.Code)
        .IsUnique();      

      modelBuilder.Entity<NonPaymentLocationType>()
        .HasIndex(c => c.Name)
        .IsUnique();

      modelBuilder.Entity<NotificationText>()
        .HasIndex(c => c.Name)
        .IsUnique();

      modelBuilder.Entity<Organisation>()
        .HasIndex(c => c.Name)
        .IsUnique();

      modelBuilder.Entity<Patient>()
        .HasIndex(p => p.AlternativeIdentifier)
        .IsUnique();
      modelBuilder.Entity<Patient>()
        .HasIndex(p => p.NhsNumber)
        .IsUnique();

      modelBuilder.Entity<PaymentMethodType>()
        .HasIndex(c => c.Name)
        .IsUnique();   

      modelBuilder.Entity<ProfileType>()
        .HasIndex(c => c.Name)
        .IsUnique();             

      modelBuilder.Entity<ReferralStatus>()
        .HasIndex(c => c.Name)
        .IsUnique();  

      modelBuilder.Entity<Section12LiveRegister>()
        .HasIndex(c => c.GmcNumber)
        .IsUnique();  

      modelBuilder.Entity<Section12ApprovalStatus>()
        .HasIndex(c => c.Name)
        .IsUnique();    

      modelBuilder.Entity<UnsuccessfulAssessmentType>()
        .HasIndex(c => c.Name)
        .IsUnique();    

      modelBuilder.Entity<User>()
        .HasIndex(c => c.GmcNumber)
        .IsUnique();   
      modelBuilder.Entity<User>()
        .HasIndex(c => c.IdentityServerIdentifier)
        .IsUnique();

      modelBuilder.Entity<UserAssessmentClaim>()
        .HasIndex(uac => uac.ClaimReference)
        .IsUnique();                                          
    }

    private void ConfigureManyToManyRelationships(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<BankDetail>()
        .HasAlternateKey(bankDetail => new
        {
          bankDetail.CcgId,
          bankDetail.UserId
        });

      modelBuilder.Entity<ContactDetail>()
        .HasAlternateKey(contextDetail => new
        {
          contextDetail.ContactDetailTypeId,
          contextDetail.UserId
        });

      modelBuilder.Entity<ContactDetailCcg>()
        .HasAlternateKey(contextDetail => new
        {
          contextDetail.CcgId,
          contextDetail.ContactDetailTypeId,
          contextDetail.UserId
        });        

      modelBuilder.Entity<AssessmentDetail>()
        .HasAlternateKey(assessmentDetail => new
        {
          assessmentDetail.AssessmentDetailTypeId,
          assessmentDetail.AssessmentId,
        });

      modelBuilder.Entity<AssessmentDoctor>()
        .HasAlternateKey(assessmentDoctor => new
        {
          assessmentDoctor.AssessmentId,
          assessmentDoctor.DoctorUserId
        });

      modelBuilder.Entity<NonPaymentLocation>()
        .HasAlternateKey(nonPaymentLocation => new
        {
          nonPaymentLocation.CcgId,
          nonPaymentLocation.NonPaymentLocationTypeId
        });        

      modelBuilder.Entity<PaymentMethod>()
        .HasAlternateKey(paymentMethod => new
        {
          paymentMethod.CcgId,
          paymentMethod.PaymentMethodTypeId,
          paymentMethod.UserId
        });

      modelBuilder.Entity<UserAssessmentClaim>()
        .HasAlternateKey(userAssessmentClaim => new
        {
          userAssessmentClaim.AssessmentId,
          userAssessmentClaim.UserId
        });

      modelBuilder.Entity<UserSpeciality>()
        .HasAlternateKey(userSpeciality => new
        {
          userSpeciality.SpecialityId,
          userSpeciality.UserId
        });

    }
  }
}
