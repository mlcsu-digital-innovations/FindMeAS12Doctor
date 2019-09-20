using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  public class TestSeeds : SeederBase
  {
    public TestSeeds(ApplicationContext context)
        : base(context)
    {
    }

    public void TestSeedAll()
    {
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