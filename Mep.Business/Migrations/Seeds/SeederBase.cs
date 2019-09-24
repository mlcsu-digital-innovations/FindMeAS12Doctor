using Mep.Data.Entities;
using System.Linq;
using System;

namespace Mep.Business.Migrations.Seeds
{
  public class SeederBase
  {

    protected GenderType _maleGender;
    protected GenderType _femaleGender;
    protected DateTimeOffset _now = DateTimeOffset.Now;

    protected const string SystemAdminIdentityServerIdentifier =
      "bf673270-2538-4e59-9d26-5b4808fd9ef6";
    protected readonly ApplicationContext _context;

    public SeederBase(ApplicationContext context)
    {
      _context = context;

      _maleGender = _context.GenderTypes.Where(gender => gender.Name == "Male").Single();
      _femaleGender = _context.GenderTypes.Where(gender => gender.Name == "Female").Single();
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