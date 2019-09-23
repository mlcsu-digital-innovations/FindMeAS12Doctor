using System;
using System.Linq;
using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  public class SeederBase
  {
    public DateTimeOffset _now = DateTimeOffset.Now;
    
    protected const string SystemAdminIdentityServerIdentifier = 
      "bf673270-2538-4e59-9d26-5b4808fd9ef6";
    protected readonly ApplicationContext _context;

    public SeederBase(ApplicationContext context)
    {
      _context = context;
    }

    protected User GetSystemAdminUser()
    {
        return _context.Users
                       .SingleOrDefault(u => 
                          u.IdentityServerIdentifier == 
                            SystemAdminIdentityServerIdentifier);
    }
  }
}