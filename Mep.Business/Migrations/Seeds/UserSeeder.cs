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

      if ((user = _context
        .Users
          .SingleOrDefault(g => g.DisplayName ==
            USER_DISPLAY_NAME_DOCTOR_FEMALE)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = USER_DISPLAY_NAME_DOCTOR_FEMALE;
      user.GenderTypeId = GetFemaleGenderTypeId();
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();
      user.IsActive = true;
      user.ModifiedByUser = GetSystemAdminUser();
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION_NAME_1);
      user.ProfileTypeId = GetProfileTypeId(PROFILE_TYPE_NAME_DOCTOR);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;

      if ((user = _context
        .Users
          .SingleOrDefault(g => g.DisplayName ==
            USER_DISPLAY_NAME_DOCTOR_MALE)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = USER_DISPLAY_NAME_DOCTOR_MALE;
      user.GenderTypeId = GetMaleGenderTypeId();
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();
      user.IsActive = true;
      user.ModifiedByUser = GetSystemAdminUser();
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION_NAME_2);
      user.ProfileTypeId = GetProfileTypeId(PROFILE_TYPE_NAME_DOCTOR);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;

      if ((user = _context
        .Users
          .SingleOrDefault(g => g.DisplayName ==
            ORGANISATION_1_USER)) == null)
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
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION_NAME_1);
      user.ProfileTypeId = GetProfileTypeId(PROFILE_TYPE_NAME_FINANCE);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;

      if ((user = _context
        .Users
          .SingleOrDefault(g => g.DisplayName ==
            ORGANISATION_2_USER)) == null)
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
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION_NAME_2);
      user.ProfileTypeId = GetProfileTypeId(PROFILE_TYPE_NAME_AMHP);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;

      if ((user = _context
        .Users
          .SingleOrDefault(g => g.DisplayName ==
            ORGANISATION_3_USER)) == null)
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
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION_NAME_3);
      user.ProfileTypeId = GetProfileTypeId(PROFILE_TYPE_NAME_DOCTOR);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;

      if ((user = _context
        .Users
          .SingleOrDefault(g => g.DisplayName ==
            ORGANISATION_4_USER)) == null)
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
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION_NAME_4);
      user.ProfileTypeId = GetProfileTypeId(PROFILE_TYPE_NAME_FINANCE);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;

      if ((user = _context
        .Users
          .SingleOrDefault(g => g.DisplayName ==
            USER_DISPLAY_NAME_AMHP)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = USER_DISPLAY_NAME_AMHP;
      user.GenderTypeId = GetFemaleGenderTypeId();
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();
      user.IsActive = true;
      user.ModifiedByUser = GetSystemAdminUser();
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION_NAME_4);
      user.ProfileTypeId = GetProfileTypeId(PROFILE_TYPE_NAME_AMHP);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;

      if ((user = _context
        .Users
          .SingleOrDefault(g => g.DisplayName ==
            USER_DISPLAY_NAME_FINANCE)) == null)
      {
        user = new User();
        _context.Add(user);
      }
      user.DisplayName = USER_DISPLAY_NAME_FINANCE;
      user.GenderTypeId = GetMaleGenderTypeId();
      user.GmcNumber = null;
      user.HasReadTermsAndConditions = true;
      user.IdentityServerIdentifier = Guid.NewGuid().ToString();
      user.IsActive = true;
      user.ModifiedByUser = GetSystemAdminUser();
      user.OrganisationId = GetOrganisationIdByName(ORGANISATION_NAME_4);
      user.ProfileTypeId = GetProfileTypeId(PROFILE_TYPE_NAME_DOCTOR);
      user.Section12ApprovalStatusId = null;
      user.Section12ExpiryDate = null;
    }
  }
}