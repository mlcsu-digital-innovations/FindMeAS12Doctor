using System;
using System.Linq;
using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class UserSeeder : SeederBase
  {

    internal UserSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      User user;

      GenderType maleGender = _context.GenderTypes.Where(gender => gender.Name == "Male").FirstOrDefault();
      GenderType femaleGender = _context.GenderTypes.Where(gender => gender.Name == "Female").FirstOrDefault();

      if ((user =
        _context.Users
          .SingleOrDefault(g => g.DisplayName == "Doctor Female")) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = "Doctor Female";
      user.GenderTypeId = femaleGender.Id;
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();
      user.IsActive = true;
      user.ModifiedByUser = GetSystemAdminUser();
      user.OrganisationId = 1;
      user.ProfileTypeId = 1;
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;

      if ((user =
        _context.Users
          .SingleOrDefault(g => g.DisplayName == "Doctor Male")) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = "Doctor Male";
      user.GenderTypeId = maleGender.Id;
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();
      user.IsActive = true;
      user.ModifiedByUser = GetSystemAdminUser();
      user.OrganisationId = 1;
      user.ProfileTypeId = 1;
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;
    }
  }
}