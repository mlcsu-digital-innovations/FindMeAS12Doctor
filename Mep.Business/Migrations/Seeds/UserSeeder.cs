using Mep.Data.Entities;
using System.Linq;
using System;

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

      if ((user =
        _context.Users
          .SingleOrDefault(g => g.DisplayName == USERDISPLAYNAME1)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = USERDISPLAYNAME1;
      user.GenderTypeId = GetFemaleGenderTypeId();
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
          .SingleOrDefault(g => g.DisplayName == USERDISPLAYNAME2)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = USERDISPLAYNAME2;
      user.GenderTypeId = GetMaleGenderTypeId();
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