using Mep.Data.Entities.Audit;
using Microsoft.EntityFrameworkCore;

namespace Mep.Business
{
    public partial class AuditContext : DbContext
  {
    public AuditContext()
    {
    }

    public AuditContext(DbContextOptions<AuditContext> options)
      : base(options)
    {
    }

    public virtual DbSet<BankDetailAudit> BankDetails { get; set; }
    public virtual DbSet<CcgAudit> Ccgs { get; set; }
    public virtual DbSet<ClaimStatusAudit> ClaimStatuses { get; set; }    
    public virtual DbSet<ContactDetailAudit> ContactDetails { get; set; }
    public virtual DbSet<ContactDetailTypeAudit> ContactDetailTypes { get; set; }
    public virtual DbSet<DoctorStatusAudit> DoctorStatuses { get; set; }
    public virtual DbSet<ExaminationAudit> Examinations { get; set; }
    public virtual DbSet<GpPracticeAudit> GpPractices { get; set; }
    public virtual DbSet<NonPaymentLocationAudit> NonPaymentLocations { get; set; }
    public virtual DbSet<NonPaymentLocationTypeAudit> NonPaymentLocationTypes { get; set; }
    public virtual DbSet<NotificationTextAudit> NotificationTexts { get; set; }
    public virtual DbSet<OnCallUserAudit> OnCallUsers { get; set; }
    public virtual DbSet<OrganisationAudit> Organisations { get; set; }
    public virtual DbSet<PatientAudit> Patients { get; set; }
    public virtual DbSet<PaymentMethodAudit> PaymentMethods { get; set; }
    public virtual DbSet<PaymentMethodTypeAudit> PaymentMethodTypes { get; set; }    
    public virtual DbSet<PaymentRuleAudit> PaymentRules { get; set; }
    public virtual DbSet<PaymentRuleSetAudit> PaymentRuleSets { get; set; }
    public virtual DbSet<ProfileTypeAudit> ProfileTypes { get; set; }
    public virtual DbSet<ReferralAudit> Referrals { get; set; }
    public virtual DbSet<ReferralStatusAudit> ReferralStatuses { get; set; }    
    public virtual DbSet<Section12ApprovalStatusAudit> Section12ApprovalStatuses { get; set; }
    public virtual DbSet<SpecialityAudit> Specialities { get; set; }
    public virtual DbSet<UnsuccessfulExaminationTypeAudit> UnsuccessfulExaminationTypes { get; set; }
    public virtual DbSet<UserAudit> Users { get; set; }
    public virtual DbSet<UserExaminationClaimAudit> UserExaminationClaims { get; set; }
    public virtual DbSet<UserExaminationNotificationAudit> UserExaminationNotifications { get; set; }
    public virtual DbSet<UserSpecialityAudit> UserSpecialities { get; set; }    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

      modelBuilder.Entity<BankDetailAudit>()
        .HasAlternateKey(bankDetailAudit => new { 
          bankDetailAudit.CcgId, 
          bankDetailAudit.UserId 
      });

      modelBuilder.Entity<ContactDetailAudit>()
        .HasAlternateKey(contextDetailAudit => new {
          contextDetailAudit.CcgId, 
          contextDetailAudit.ContactDetailTypeId, 
          contextDetailAudit.UserId
      });

      modelBuilder.Entity<PaymentMethodAudit>()
        .HasAlternateKey(paymentMethodAudit => new {
          paymentMethodAudit.CcgId,
          paymentMethodAudit.PaymentMethodTypeId,
          paymentMethodAudit.UserId
      });

      modelBuilder.Entity<UserExaminationNotificationAudit>()
        .HasAlternateKey(userExaminationNotificationAudit => new {
          userExaminationNotificationAudit.ExaminationId,
          userExaminationNotificationAudit.UserId
        });
    }
  }
}
