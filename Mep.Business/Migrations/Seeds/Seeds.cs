using Microsoft.Extensions.Configuration;

namespace Mep.Business.Migrations.Seeds
{
  public class Seeds : SeederBaseBase
  {
    public Seeds(ApplicationContext context, IConfiguration config)
    {
      _config = config;
      _context = context;
    }

    public void SeedAll(bool noGpPractices, bool noCcgs)
    {
      if (!noCcgs)
      {
        new CcgSeeder().SeedData();
        _context.SaveChanges();
      }

      if (!noGpPractices)
      {
        new GpPracticeSeeder().SeedData();
        _context.SaveChanges();
      }

      new ClaimStatusesSeeder().SeedData();

      new ContactDetailTypesSeeder().SeedData();

      new ExaminationDetailTypeSeeder().SeedData();

      new GenderTypeSeeder().SeedData();

      new NonPaymentLocationTypesSeeder().SeedData();

      new NotificationTextsSeeder().SeedData();      

      new PaymentMethodTypesSeeder().SeedData();

      new PaymentRuleSetsSeeder().SeedData();

      new ProfileTypeSeeder().SeedData();

      new ReferralStatusSeeder().SeedData();

      new Section12ApprovalStatusesSeeder().SeedData();

      new SpecialitySeeder().SeedData();

      new UnsuccessfulExaminationTypesSeeder().SeedData();

      _context.SaveChanges();
    }

    public void RemoveSeedAll(bool noGpPractices, bool noCcgs)
    {
      RemoveSeedTestAll();

      if (!noCcgs)
      {
        new CcgSeeder().DeleteSeeds();
        _context.SaveChanges();
      }

      if (!noGpPractices)
      {
        new GpPracticeSeeder().DeleteSeeds();
        _context.SaveChanges();
      }

      new ClaimStatusesSeeder().DeleteSeeds();

      new ContactDetailTypesSeeder().DeleteSeeds();

      new ExaminationDetailTypeSeeder().DeleteSeeds();

      new GenderTypeSeeder().DeleteSeeds();

      new NonPaymentLocationTypesSeeder().DeleteSeeds();

      new NotificationTextsSeeder().DeleteSeeds();      

      new PaymentMethodTypesSeeder().DeleteSeeds();

      new PaymentRuleSetsSeeder().DeleteSeeds();

      new ProfileTypeSeeder().DeleteSeeds();

      new ReferralStatusSeeder().DeleteSeeds();

      new Section12ApprovalStatusesSeeder().DeleteSeeds();

      new SpecialitySeeder().DeleteSeeds();

      new UnsuccessfulExaminationTypesSeeder().DeleteSeeds();

      _context.SaveChanges();
    }    

    public void SeedTestAll()
    {
      new OrganisationSeeder().SeedData();
      _context.SaveChanges();

      new UserSeeder().SeedData();
      _context.SaveChanges();

      new DoctorStatusesSeeder().SeedData();
      _context.SaveChanges();

      new PatientSeeder().SeedData();
      _context.SaveChanges();

      new ReferralSeeder().SeedData();
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

    public void RemoveSeedTestAll()
    {
      new UserSpecialitiesSeeder().DeleteSeeds();
      _context.SaveChanges();

      new PaymentRulesSeeder().DeleteSeeds();
      _context.SaveChanges();

      new PaymentMethodsSeeder().DeleteSeeds();
      _context.SaveChanges();

      new OnCallUsersSeeder().DeleteSeeds();
      _context.SaveChanges();

      new NonPaymentLocationsSeeder().DeleteSeeds();
      _context.SaveChanges();

      new ContactDetailsSeeder().DeleteSeeds();
      _context.SaveChanges();

      new BankDetailsSeeder().DeleteSeeds();
      _context.SaveChanges();

      new UserExaminationClaimsSeeder().DeleteSeeds();
      _context.SaveChanges();

      new UserExaminationNotificationSeeder().DeleteSeeds();
      _context.SaveChanges();

      new ExaminationSeeder().DeleteSeeds();
      _context.SaveChanges();

      new ReferralSeeder().DeleteSeeds();
      _context.SaveChanges();

      new PatientSeeder().DeleteSeeds();
      _context.SaveChanges();

      new DoctorStatusesSeeder().DeleteSeeds();
      _context.SaveChanges();

      new UserSeeder().DeleteSeeds();
      _context.SaveChanges();

      new OrganisationSeeder().DeleteSeeds();
      _context.SaveChanges();
    }
  }
}