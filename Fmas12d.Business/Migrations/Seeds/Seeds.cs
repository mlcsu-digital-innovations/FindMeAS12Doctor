using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Fmas12d.Business.Migrations.Seeds
{
  public class Seeds : SeederBaseBase
  {
    public Seeds(ApplicationContext context, IConfiguration config)
    {
      Config = config;
      Context = context;
    }

    public void SeedAll(bool noGpPractices, bool noCcgs)
    {
      if (!noCcgs)
      {
        new CcgSeeder().SeedData();
        Context.SaveChanges();
      }

      if (!noGpPractices)
      {
        new GpPracticeSeeder().SeedData();
        Context.SaveChanges();
      }

      new ClaimStatusesSeeder().SeedData();

      new ContactDetailTypesSeeder().SeedData();

      new AssessmentDetailTypesSeeder().SeedData();

      new AssessmentDoctorStatusSeeder().SeedData();

      new GenderTypesSeeder().SeedData();

      new NonPaymentLocationTypesSeeder().SeedData();

      new NotificationTextsSeeder().SeedData();

      new PaymentMethodTypesSeeder().SeedData();

      new PaymentRuleSetsSeeder().SeedData();

      new ProfileTypesSeeder().SeedData();

      new ReferralStatusesSeeder().SeedData();

      new Section12ApprovalStatusesSeeder().SeedData();

      new SpecialitiesSeeder().SeedData();

      new UnsuccessfulAssessmentTypesSeeder().SeedData();
    }

    public void RemoveSeedAll(bool noGpPractices, bool noCcgs)
    {
      RemoveSeedTestAll();

      if (!noCcgs)
      {
        new CcgSeeder().DeleteSeeds();
        Context.SaveChanges();
      }

      if (!noGpPractices)
      {
        new GpPracticeSeeder().DeleteSeeds();
        Context.SaveChanges();
      }

      new ClaimStatusesSeeder().DeleteSeeds();

      new ContactDetailTypesSeeder().DeleteSeeds();

      new AssessmentDetailTypesSeeder().DeleteSeeds();

      new GenderTypesSeeder().DeleteSeeds();

      new NonPaymentLocationTypesSeeder().DeleteSeeds();

      new NotificationTextsSeeder().DeleteSeeds();

      new PaymentMethodTypesSeeder().DeleteSeeds();

      new PaymentRuleSetsSeeder().DeleteSeeds();

      new ProfileTypesSeeder().DeleteSeeds();

      new ReferralStatusesSeeder().DeleteSeeds();

      new Section12ApprovalStatusesSeeder().DeleteSeeds();

      new SpecialitiesSeeder().DeleteSeeds();

      new UnsuccessfulAssessmentTypesSeeder().DeleteSeeds();

      Context.SaveChanges();
    }

    public void SeedTestAll()
    {
      new OrganisationSeeder().SeedData();
      Context.SaveChanges();

      new UserSeeder().SeedData();
      Context.SaveChanges();

      new DoctorStatusesSeeder().SeedData();
      Context.SaveChanges();

      new PatientSeeder().SeedData();
      Context.SaveChanges();

      new ReferralSeeder().SeedData();
      Context.SaveChanges();

      new BankDetailsSeeder().SeedData();
      Context.SaveChanges();

      new ContactDetailsSeeder().SeedData();
      Context.SaveChanges();

      new NonPaymentLocationsSeeder().SeedData();
      Context.SaveChanges();

      new OnCallUsersSeeder().SeedData();
      Context.SaveChanges();

      new PaymentMethodsSeeder().SeedData();
      Context.SaveChanges();

      new PaymentRulesSeeder().SeedData();
      Context.SaveChanges();

      new UserSpecialitiesSeeder().SeedData();
      Context.SaveChanges();
    }

    public void RemoveSeedTestAll()
    {
      new UserSpecialitiesSeeder().DeleteSeeds();
      Context.SaveChanges();

      new PaymentRulesSeeder().DeleteSeeds();
      Context.SaveChanges();

      new PaymentMethodsSeeder().DeleteSeeds();
      Context.SaveChanges();

      new OnCallUsersSeeder().DeleteSeeds();
      Context.SaveChanges();

      new NonPaymentLocationsSeeder().DeleteSeeds();
      Context.SaveChanges();

      new ContactDetailsSeeder().DeleteSeeds();
      Context.SaveChanges();

      new BankDetailsSeeder().DeleteSeeds();
      Context.SaveChanges();

      new UserAssessmentClaimsSeeder().DeleteSeeds();
      Context.SaveChanges();

      new UserAssessmentNotificationSeeder().DeleteSeeds();
      Context.SaveChanges();

      new AssessmentSeeder().DeleteSeeds();
      Context.SaveChanges();

      new ReferralSeeder().DeleteSeeds();
      Context.SaveChanges();

      new PatientSeeder().DeleteSeeds();
      Context.SaveChanges();

      new DoctorStatusesSeeder().DeleteSeeds();
      Context.SaveChanges();

      new UserSeeder().DeleteSeeds();
      Context.SaveChanges();

      new OrganisationSeeder().DeleteSeeds();
      Context.SaveChanges();
    }
  }
}