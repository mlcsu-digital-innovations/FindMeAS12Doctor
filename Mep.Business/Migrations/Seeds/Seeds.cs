using Microsoft.Extensions.Configuration;

namespace Mep.Business.Migrations.Seeds
{
  public class Seeds : SeederDoubleBase
  {
    public Seeds(ApplicationContext context, IConfiguration config)
    {
      _config = config;
      _context = context;
    }

    public void SeedAll(bool noGpPractices)
    {
      new CcgSeeder().SeedData();
      _context.SaveChanges();

      if (!noGpPractices)
      {
        new GpPracticeSeeder().SeedData();
        _context.SaveChanges();
      }

      new ClaimStatusesSeeder().SeedData();

      new ContactDetailTypesSeeder().SeedData();

      new DoctorStatusesSeeder().SeedData();

      new ExaminationDetailTypeSeeder().SeedData();

      new GenderTypeSeeder().SeedData();

      new NonPaymentLocationTypesSeeder().SeedData();

      new PaymentMethodTypesSeeder().SeedData();

      new PaymentRuleSetsSeeder().SeedData();

      new ProfileTypeSeeder().SeedData();

      new ReferralStatusSeeder().SeedData();

      new Section12ApprovalStatusesSeeder().SeedData();

      new SpecialitySeeder().SeedData();

      new UnsuccessfulExaminationTypesSeeder().SeedData();

      _context.SaveChanges();
    }

    public void SeedTestAll()
    {
      new OrganisationSeeder().SeedData();
      _context.SaveChanges();

      new UserSeeder().SeedData();
      _context.SaveChanges();

      new PatientSeeder().SeedData();
      _context.SaveChanges();

      new NotificationTextsSeeder().SeedData();
      _context.SaveChanges();

      new ReferralSeeder().SeedData();
      _context.SaveChanges();

      new ExaminationSeeder().SeedData();
      _context.SaveChanges();

      new UserExaminationNotificationSeeder().SeedData();
      _context.SaveChanges();

      new UserExaminationClaimsSeeder().SeedData();
      _context.SaveChanges();

      new BankDetailsSeeder().SeedData();
      _context.SaveChanges();

      new ContactDetailsSeeder().SeedData();
      _context.SaveChanges();

      new NonPaymentLocationsSeeder().SeedData();
      _context.SaveChanges();

      new OnCallUsersSeeder().SeedData();
      _context.SaveChanges();

      new PaymentMethodsSeeder().SeedData();
      _context.SaveChanges();

      new PaymentRulesSeeder().SeedData();
      _context.SaveChanges();

      new UserSpecialitiesSeeder().SeedData();
      _context.SaveChanges();
    }

  }
}