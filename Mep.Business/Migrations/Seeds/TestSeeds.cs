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

      new PatientSeeder(_context).SeedData();
      
      new NotificationTextsSeeder(_context).SeedData();

      new ReferralSeeder(_context).SeedData();

      new ExaminationSeeder(_context).SeedData();

      new UserExaminationNotificationSeeder(_context).SeedData();

      new UserExaminationClaimSeeder(_context).SeedData();

      _context.SaveChanges();
    }
  }
}