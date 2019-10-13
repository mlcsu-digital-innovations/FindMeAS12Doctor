using System;
using Microsoft.Extensions.Configuration;

namespace Mep.Business.Migrations.Seeds
{
  public class Seeds : SeederBase
  {
    public Seeds(ApplicationContext context, IConfiguration config)
    {
      _config = config;
      _context = context;
    }

    public void SeedAll(bool noGp)
    {
      new CcgSeeder().SeedData();
      _context.SaveChanges();

      if (!noGp)
      {
        new GpPracticeSeeder().SeedData();
        _context.SaveChanges();
      }

      // new ClaimStatusesSeeder(_context).SeedData();

      // new ContactDetailTypesSeeder(_context).SeedData();

      // new DoctorStatusesSeeder(_context).SeedData();

      // new ExaminationDetailTypeSeeder(_context).SeedData();

      // new GenderTypeSeeder(_context).SeedData();

      // new NonPaymentLocationTypesSeeder(_context).SeedData();

      // new PaymentMethodTypesSeeder(_context).SeedData();

      // new PaymentRuleSetsSeeder(_context).SeedData();

      // new ProfileTypeSeeder(_context).SeedData();

      // new ReferralStatusSeeder(_context).SeedData();

      // new Section12ApprovalStatusesSeeder(_context).SeedData();

      // new SpecialitySeeder(_context).SeedData();

      // new UnsuccessfulExaminationTypesSeeder(_context).SeedData();

      // _context.SaveChanges();
    }

    public void SeedTestAll()
    {
      new OrganisationSeeder(_context).SeedData();
      _context.SaveChanges();

      new UserSeeder(_context).SeedData();
      _context.SaveChanges();

      new PatientSeeder(_context).SeedData();
      _context.SaveChanges();

      new NotificationTextsSeeder(_context).SeedData();
      _context.SaveChanges();

      new ReferralSeeder(_context).SeedData();
      _context.SaveChanges();

      new ExaminationSeeder(_context).SeedData();
      _context.SaveChanges();

      new UserExaminationNotificationSeeder(_context).SeedData();
      _context.SaveChanges();

      new UserExaminationClaimsSeeder(_context).SeedData();
      _context.SaveChanges();

      new BankDetailsSeeder(_context).SeedData();
      _context.SaveChanges();

      new ContactDetailsSeeder(_context).SeedData();
      _context.SaveChanges();

      new NonPaymentLocationsSeeder(_context).SeedData();
      _context.SaveChanges();

      new OnCallUsersSeeder(_context).SeedData();
      _context.SaveChanges();

      new PaymentMethodsSeeder(_context).SeedData();
      _context.SaveChanges();

      new PaymentRulesSeeder(_context).SeedData();
      _context.SaveChanges();

      new UserSpecialitiesSeeder(_context).SeedData();
      _context.SaveChanges();
    }

  }
}