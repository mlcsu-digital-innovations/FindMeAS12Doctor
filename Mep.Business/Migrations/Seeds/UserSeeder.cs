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
          .SingleOrDefault(g => g.DisplayName == USERDISPLAYNAMEFEMALE)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = USERDISPLAYNAMEFEMALE;
      user.GenderTypeId = GetFemaleGenderTypeId();
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();
      user.IsActive = true;
      user.ModifiedByUser = GetSystemAdminUser();
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION1);
      user.ProfileTypeId = GetProfileTypeId(PROFILETYPE1);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;

      if ((user =
        _context.Users
          .SingleOrDefault(g => g.DisplayName == USERDISPLAYNAMEMALE)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = USERDISPLAYNAMEMALE;
      user.GenderTypeId = GetMaleGenderTypeId();
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();
      user.IsActive = true;
      user.ModifiedByUser = GetSystemAdminUser();
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION2);
      user.ProfileTypeId = GetProfileTypeId(PROFILETYPE2);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;

      if ((user =
        _context.Users
          .SingleOrDefault(g => g.DisplayName == ORGANISATION1USER)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = ORGANISATION1USER;
      user.GenderTypeId = GetMaleGenderTypeId();
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();
      user.IsActive = true;
      user.ModifiedByUser = GetSystemAdminUser();
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION1);
      user.ProfileTypeId = GetProfileTypeId(PROFILETYPE3);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;
            
      if ((user =
        _context.Users
          .SingleOrDefault(g => g.DisplayName == ORGANISATION2USER)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = ORGANISATION2USER;
      user.GenderTypeId = GetFemaleGenderTypeId();
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();
      user.IsActive = true;
      user.ModifiedByUser = GetSystemAdminUser();
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION2);
      user.ProfileTypeId = GetProfileTypeId(PROFILETYPE1);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;
            
      if ((user =
        _context.Users
          .SingleOrDefault(g => g.DisplayName == ORGANISATION3USER)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = ORGANISATION3USER;
      user.GenderTypeId = GetMaleGenderTypeId();
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();
      user.IsActive = true;
      user.ModifiedByUser = GetSystemAdminUser();
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION3);
      user.ProfileTypeId = GetProfileTypeId(PROFILETYPE2);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;
            
      if ((user =
        _context.Users
          .SingleOrDefault(g => g.DisplayName == ORGANISATION4USER)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = ORGANISATION4USER;
      user.GenderTypeId = GetFemaleGenderTypeId();
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();
      user.IsActive = true;
      user.ModifiedByUser = GetSystemAdminUser();
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION3);
      user.ProfileTypeId = GetProfileTypeId(PROFILETYPE3);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;
    }
  }
}