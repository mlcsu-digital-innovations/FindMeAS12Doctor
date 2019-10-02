namespace Mep.Business.Migrations.Seeds
{
  public class Seeds : SeederBase
  {
    public Seeds(ApplicationContext context)
        : base(context)
    {
    }

    public void SeedAll()
    {
      new SystemAdminUserSeeder(_context).SeedData();
      _context.SaveChanges();

      // create all CCGs
      new CcgSeeder(_context).SeedData();
      _context.SaveChanges();

      // create all GP Practices
      new GpPracticeSeeder(_context).SeedData();
      _context.SaveChanges();

      new ClaimStatusesSeeder(_context).SeedData();

      new ContactDetailsSeeder(_context).SeedData();

      new DoctorStatusesSeeder(_context).SeedData();

      new GenderTypeSeeder(_context).SeedData();

      new NonPaymentLocationTypesSeeder(_context).SeedData();

      new PaymentMethodTypesSeeder(_context).SeedData();

      new PaymentRuleSetsSeeder(_context).SeedData();

      new ProfileTypeSeeder(_context).SeedData();

      new ReferralStatusSeeder(_context).SeedData();

      new Section12ApprovalStatusesSeeder(_context).SeedData();

      new SpecialitySeeder(_context).SeedData();

      new UnsuccessfulExaminationTypesSeeder(_context).SeedData();

      new UserSpecialitiesSeeder(_context).SeedData();

      _context.SaveChanges();
    }

    // all seeders apart from GpPracticeSeeder
    public void SeedAllNoGp()
    {
      new SystemAdminUserSeeder(_context).SeedData();
      _context.SaveChanges();

      // create all CCGs
      new CcgSeeder(_context).SeedData();
      _context.SaveChanges();

      new ClaimStatusesSeeder(_context).SeedData();

      new ContactDetailsSeeder(_context).SeedData();

      new DoctorStatusesSeeder(_context).SeedData();

      new GenderTypeSeeder(_context).SeedData();

      new NonPaymentLocationTypesSeeder(_context).SeedData();

      new PaymentMethodTypesSeeder(_context).SeedData();

      new PaymentRuleSetsSeeder(_context).SeedData();

      new ProfileTypeSeeder(_context).SeedData();

      new ReferralStatusSeeder(_context).SeedData();

      new Section12ApprovalStatusesSeeder(_context).SeedData();

      new SpecialitySeeder(_context).SeedData();

      new UnsuccessfulExaminationTypesSeeder(_context).SeedData();
      
      new UserSpecialitiesSeeder(_context).SeedData();

      _context.SaveChanges();
    }

    // run test seeders
    public void TestSeedAll()
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

      new UserExaminationClaimSeeder(_context).SeedData();
      _context.SaveChanges();
    }
  }
}