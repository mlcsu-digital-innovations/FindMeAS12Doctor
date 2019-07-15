namespace Mep.Business.Migrations.Seeds
{
    public class SeederBase
    {
        protected readonly ApplicationContext _context;

        public SeederBase(ApplicationContext context)
        {
            _context = context;
        }
    }
}