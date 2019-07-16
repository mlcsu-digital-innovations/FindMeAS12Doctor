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

            _context.SaveChanges();
        }        
    }
}