using Mep.Data.Entities;
using System.Linq;
using System;

namespace Mep.Business.Migrations.Seeds
{
  internal class UserSeeder : SeederBase
  {
    public GenderType MaleGender { 
      get {
        return _maleGender;
      }
    }
    public GenderType FemaleGender { 
      get {
        return _femaleGender;
      }
    }

    internal UserSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      User user;

      if ((user =
        _context.Users
          .SingleOrDefault(g => g.DisplayName == "Doctor Female")) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = "Doctor Female";
      user.GenderTypeId = FemaleGender.Id;
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
      user.GenderTypeId = MaleGender.Id;
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