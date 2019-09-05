using Mep.Data.Entities;

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
      // create all CCGs
      new CcgSeeder(_context).SeedData();
      _context.SaveChanges();

      // create all GP Practices
      new GpPracticeSeeder(_context).SeedData();
      _context.SaveChanges();

      new GenderTypeSeeder(_context).SeedData();

      new SystemAdminUserSeeder(_context).SeedData();

      new SpecialitySeeder(_context).SeedData();

      // new PatientSeeder(_context).SeedData();

      new ReferralStatusSeeder(_context).SeedData();

      _context.SaveChanges();
    }
  }
}