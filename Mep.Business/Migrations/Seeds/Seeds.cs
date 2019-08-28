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
            new SystemAdminUserSeeder(_context).SeedData();
            
            new SpecialitySeeder(_context).SeedData();

            new CcgSeeder(_context).SeedData();
            new GpPracticeSeeder(_context).SeedData();
            new PatientSeeder(_context).SeedData();

            _context.SaveChanges();
        }        
    }
}