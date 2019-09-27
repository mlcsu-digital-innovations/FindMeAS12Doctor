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
          .SingleOrDefault(g => g.DisplayName == USER_DISPLAY_NAME_FEMALE)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = USER_DISPLAY_NAME_FEMALE;
      user.GenderTypeId = GetFemaleGenderTypeId();
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();
      user.IsActive = true;
      user.ModifiedByUser = GetSystemAdminUser();
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION_1);
      user.ProfileTypeId = GetProfileTypeId(PROFILE_TYPE_1);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;

      if ((user =
        _context.Users
          .SingleOrDefault(g => g.DisplayName == USER_DISPLAY_NAME_MALE)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = USER_DISPLAY_NAME_MALE;
      user.GenderTypeId = GetMaleGenderTypeId();
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();
      user.IsActive = true;
      user.ModifiedByUser = GetSystemAdminUser();
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION_2);
      user.ProfileTypeId = GetProfileTypeId(PROFILE_TYPE_2);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;

      if ((user =
        _context.Users
          .SingleOrDefault(g => g.DisplayName == ORGANISATION_1_USER)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = ORGANISATION_1_USER;
      user.GenderTypeId = GetMaleGenderTypeId();
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();
      user.IsActive = true;
      user.ModifiedByUser = GetSystemAdminUser();
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION_1);
      user.ProfileTypeId = GetProfileTypeId(PROFILE_TYPE_3);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;
            
      if ((user =
        _context.Users
          .SingleOrDefault(g => g.DisplayName == ORGANISATION_2_USER)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = ORGANISATION_2_USER;
      user.GenderTypeId = GetFemaleGenderTypeId();
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();
      user.IsActive = true;
      user.ModifiedByUser = GetSystemAdminUser();
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION_2);
      user.ProfileTypeId = GetProfileTypeId(PROFILE_TYPE_1);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;
            
      if ((user =
        _context.Users
          .SingleOrDefault(g => g.DisplayName == ORGANISATION_3_USER)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = ORGANISATION_3_USER;
      user.GenderTypeId = GetMaleGenderTypeId();
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();
      user.IsActive = true;
      user.ModifiedByUser = GetSystemAdminUser();
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION_3);
      user.ProfileTypeId = GetProfileTypeId(PROFILE_TYPE_2);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;
            
      if ((user =
        _context.Users
          .SingleOrDefault(g => g.DisplayName == ORGANISATION_4_USER)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = ORGANISATION_4_USER;
      user.GenderTypeId = GetFemaleGenderTypeId();
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();
      user.IsActive = true;
      user.ModifiedByUser = GetSystemAdminUser();
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION_3);
      user.ProfileTypeId = GetProfileTypeId(PROFILE_TYPE_3);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;
    }
  }
}