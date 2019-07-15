using Mep.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mep.Business
{
    public partial class ApplicationContext : DbContext
  {
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
      : base(options)
    {
    }

    public virtual DbSet<BankDetail> BankDetails { get; set; }
    public virtual DbSet<Ccg> Ccgs { get; set; }
    public virtual DbSet<ClaimStatus> ClaimStatuses { get; set; }    
    public virtual DbSet<ContactDetail> ContactDetails { get; set; }
    public virtual DbSet<ContactDetailType> ContactDetailTypes { get; set; }
    public virtual DbSet<DoctorStatus> DoctorStatuses { get; set; }
    public virtual DbSet<Examination> Examinations { get; set; }
    public virtual DbSet<GpPractice> GpPractices { get; set; }
    public virtual DbSet<NonPaymentLocation> NonPaymentLocations { get; set; }
    public virtual DbSet<NonPaymentLocationType> NonPaymentLocationTypes { get; set; }
    public virtual DbSet<NotificationText> NotificationTexts { get; set; }
    public virtual DbSet<OnCallUser> OnCallUsers { get; set; }
    public virtual DbSet<Organisation> Organisations { get; set; }
    public virtual DbSet<Patient> Patients { get; set; }
    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
    public virtual DbSet<PaymentMethodType> PaymentMethodTypes { get; set; }    
    public virtual DbSet<PaymentRule> PaymentRules { get; set; }
    public virtual DbSet<PaymentRuleSet> PaymentRuleSets { get; set; }
    public virtual DbSet<ProfileType> ProfileTypes { get; set; }
    public virtual DbSet<Referral> Referrals { get; set; }
    public virtual DbSet<ReferralStatus> ReferralStatuses { get; set; }    
    public virtual DbSet<Section12ApprovalStatus> Section12ApprovalStatuses { get; set; }
    public virtual DbSet<Speciality> Specialities { get; set; }
    public virtual DbSet<UnsuccessfulExaminationType> UnsuccessfulExaminationTypes { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserExaminationClaim> UserExaminationClaims { get; set; }
    public virtual DbSet<UserExaminationNotification> UserExaminationNotifications { get; set; }
    public virtual DbSet<UserSpeciality> UserSpecialities { get; set; }    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

      modelBuilder.Entity<BankDetail>()
        .HasAlternateKey(bankDetail => new { 
          bankDetail.CcgId, 
          bankDetail.UserId 
      });

      modelBuilder.Entity<ContactDetail>()
        .HasAlternateKey(contextDetail => new {
          contextDetail.CcgId, 
          contextDetail.ContactDetailTypeId, 
          contextDetail.UserId
      });

      modelBuilder.Entity<PaymentMethod>()
        .HasAlternateKey(paymentMethod => new {
          paymentMethod.CcgId,
          paymentMethod.PaymentMethodTypeId,
          paymentMethod.UserId
      });

      modelBuilder.Entity<UserExaminationNotification>()
        .HasAlternateKey(userExaminationNotification => new {
          userExaminationNotification.ExaminationId,
          userExaminationNotification.UserId
        });
    }
  }
}
