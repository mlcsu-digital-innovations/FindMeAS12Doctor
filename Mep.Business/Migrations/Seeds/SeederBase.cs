using Mep.Data.Entities;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace Mep.Business.Migrations.Seeds
{
  public class SeederBase<TEntity> : SeederBaseBase where TEntity : BaseEntity, new()
  {
    protected DateTimeOffset _now = DateTimeOffset.Now;
    private User _systemAdminUser = null;

    public SeederBase() { }

    protected TEntity AddOrUpdateNameDescriptionEntityById(int id, string name, string description)
    {
      TEntity entity;

      if ((entity = Context.Set<TEntity>().SingleOrDefault(e => e.Id == id)) == null)
      {
        entity = new TEntity();
        Context.Add(entity);
      }
      PopulateNameDescriptionAndActiveAndModifiedWithSystemUser(entity, name, description);

      return entity;
    }

    internal virtual void DeleteSeeds()
    {
      Context.Set<TEntity>().RemoveRange(
        Context.Set<TEntity>().ToList()
      );
      ResetIdentity();
    }   

    internal virtual void ResetIdentity(int newReseedValue = 0)
    {
      string tableName = Context.Model.GetEntityTypes()
        .First(t => t.ClrType == typeof(TEntity)).GetAnnotations()
        .First(a => a.Name == "Relational:TableName").Value.ToString();

      Context.Database.ExecuteSqlRaw(
        "IF EXISTS (SELECT * FROM sys.identity_columns WHERE " +
        $"object_id = OBJECT_ID('dbo.{tableName}') AND last_value IS NOT NULL) " +
        $"DBCC CHECKIDENT('{tableName}', RESEED, {newReseedValue})");
    }

    protected Ccg GetCcgByName(string CcgName)
    {
      try
      {
        return Context.Ccgs.Single(ccg => ccg.Name == CcgName);
      }
      catch (Exception ex)
      {
        throw new ArgumentException($"Cannot find a CCG with the name {CcgName} in Ccgs", ex);
      }
    }

    protected int GetClaimStatusIdByClaimStatusName(string ClaimStatusName)
    {
      try
      {
        return Context
          .ClaimStatuses
            .Single(claimStatus => claimStatus.Name == ClaimStatusName).Id;
      }
      catch (Exception ex)
      {
        throw new ArgumentException($"Cannot find a Claim Status with the name {ClaimStatusName} in ClaimStatuses", ex);
      }
    }

    protected int GetContactDetailTypeIdByContactDetailTypeName(string ContactDetailTypeName)
    {
      try
      {
        return Context
          .ContactDetailTypes
            .Single(contactDetailType => contactDetailType.Name == ContactDetailTypeName).Id;
      }
      catch (Exception ex)
      {
        throw new ArgumentException($"Cannot find a Contact Detail Type with the name of {ContactDetailTypeName} in ContactDetailTypes", ex);
      }
    }


    protected ContactDetailType GetContactDetailTypeById(int id)
    {
      try
      {
        return Context.ContactDetailTypes.Single(c => c.Id == id);
      }
      catch (Exception ex)
      {
        throw new ArgumentException(
          $"Cannot find a Contact Detail Type with an id of {id} in ContactDetailTypes",
          ex);
      }
    }

    protected ContactDetailType GetContactDetailTypeWork()
    {
      return GetContactDetailTypeById(Models.ContactDetailType.WORK);
    }

    protected int GetExaminationIdByExaminationAddress(string examinationAddress)
    {
      try
      {
        return Context
          .Examinations
            .Single(examination => examination.Address1 == examinationAddress).Id;
      }
      catch (Exception ex)
      {
        throw new ArgumentException(
          $"Cannot find examination with an Address1 of {examinationAddress} in Examinations", ex);
      }
    }

    protected GenderType GetGenderTypeById(int id)
    {
      try
      {
        return Context.GenderTypes.Single(gt => gt.Id == id);
      }
      catch (Exception ex)
      {
        throw new ArgumentException($"Cannot find Gender Type with and id of {id} in GenderTypes", ex);
      }
    }
    protected GenderType GetGenderTypeFemale()
    {
      return GetGenderTypeById(Models.GenderType.FEMALE);
    }
    protected GenderType GetGenderTypeMale()
    {
      return GetGenderTypeById(Models.GenderType.MALE);
    }
    protected GenderType GetGenderTypeOther()
    {
      return GetGenderTypeById(Models.GenderType.OTHER);
    }
    protected GpPractice GetGpPracticeByName(string gpPracticeName)
    {
      try
      {

        return Context.GpPractices.Single(gpPractice => gpPractice.Name == gpPracticeName);
      }
      catch (Exception ex)
      {
        throw new ArgumentException(
          $"Cannot find a GP Practice with the name {gpPracticeName} in GpPractices", ex);
      }
    }

    protected int GetNonPaymentLocationTypeIdByNonPaymentLocationTypeName(string nonPaymentLocationTypeName)
    {
      try
      {
        return Context
          .NonPaymentLocationTypes
            .Single(nonPaymentLocationType => nonPaymentLocationType.Name == nonPaymentLocationTypeName).Id;
      }
      catch (Exception ex)
      {
        throw new ArgumentException(
          $"Cannot find Non Payment Location Type with the name of {nonPaymentLocationTypeName} in NonPaymentLocationTypes", ex);
      }
    }

    protected int GetNotificationTextId(string notificationTextName)
    {
      try
      {
        return Context
          .NotificationTexts
            .Single(notificationText => notificationText.Name == notificationTextName).Id;
      }
      catch (Exception ex)
      {
        throw new ArgumentException($"Cannot find {notificationTextName} in NotificationTexts", ex);
      }
    }

    protected int GetOrganisationIdByName(string name)
    {
      try
      {
        return Context
          .Organisations
            .Single(organisation => organisation.Name == name).Id;
      }
      catch (Exception ex)
      {
        throw new ArgumentException($"Cannot find an organisation with the Name {name} in Organisations", ex);
      }
    }

    protected int GetPatientIdByAlternativeIdentifier(string alternativeIdentifier)
    {
      try
      {
        return Context
          .Patients
            .Single(patient => patient.AlternativeIdentifier == alternativeIdentifier).Id;
      }
      catch (Exception ex)
      {
        throw new ArgumentException(
          $"Cannot find a Patient with an Alternative Identifier of {alternativeIdentifier} in Patients", ex);
      }
    }

    protected int GetPatientIdByNhsNumber(long nhsNumber)
    {
      try
      {
        return Context
          .Patients
            .Single(patient => patient.NhsNumber == nhsNumber).Id;
      }
      catch (Exception ex)
      {
        throw new ArgumentException(
          $"Cannot find a Patient with an NHS Number of {nhsNumber} in Patients", ex);
      }
    }

    protected int GetPaymentMethodTypeIdByPaymentMethodTypeName(string paymentMethodTypeName)
    {
      try
      {
        return Context
          .PaymentMethodTypes
            .Single(paymentMethodType => paymentMethodType.Name == paymentMethodTypeName).Id;
      }
      catch (Exception ex)
      {
        throw new ArgumentException(
          $"Cannot find Payment Method Type with the name of {paymentMethodTypeName} in PaymentMethodTypes", ex);
      }
    }

    protected int GetPaymentRuleSetIdByPaymentRuleSetName(string paymentRuleSetName)
    {
      try
      {
        return Context
          .PaymentRuleSets
            .Single(paymentRuleSet => paymentRuleSet.Name == paymentRuleSetName).Id;
      }
      catch (Exception ex)
      {
        throw new ArgumentException(
          $"Cannot find Payment Rule Set with the name of {paymentRuleSetName} in PaymentRuleSets", ex);
      }
    }

    protected int GetProfileTypeId(string profileTypeName)
    {
      try
      {
        return Context
          .ProfileTypes
            .Single(profileType => profileType.Name == profileTypeName).Id;
      }
      catch (Exception ex)
      {
        throw new ArgumentException(
          $"Cannot find a Profile Type with the name {profileTypeName} in ProfileTypes", ex);
      }
    }

    protected ProfileType GetProfileTypeById(int id)
    {
      try
      {
        return Context.ProfileTypes
          .Single(profileType => profileType.Id == id);
      }
      catch (Exception ex)
      {
        throw new ArgumentException(
          $"Cannot find a Profile Type with an id of {id} in ProfileTypes", ex);
      }
    }
    protected ProfileType GetProfileTypeAmhp()
    {
      return GetProfileTypeById(Models.ProfileType.AMHP);
    }

    protected ProfileType GetProfileTypeDoctor()
    {
      return GetProfileTypeById(Models.ProfileType.DOCTOR);
    }

    protected ProfileType GetProfileTypeFinance()
    {
      return GetProfileTypeById(Models.ProfileType.FINANCE);
    }
    protected ProfileType GetProfileTypeSystem()
    {
      return GetProfileTypeById(Models.ProfileType.SYSTEM);
    }

    protected int GetReferralIdByAlternativeIdentifier(string alternativeIdentifier)
    {
      try
      {
        return Context
          .Referrals
            .Single(referral => referral.PatientId == GetPatientIdByAlternativeIdentifier(alternativeIdentifier)).Id;
      }
      catch (Exception ex)
      {
        throw new ArgumentException(
          $"Cannot find a referral that has a Patient with an Alternative Identifier of {alternativeIdentifier} in Referrals", ex);
      }
    }

    protected int GetReferralIdByPatientNhsNumber(long nhsNumber)
    {
      try
      {
        return Context
          .Referrals
            .Single(referral => referral.PatientId == GetPatientIdByNhsNumber(nhsNumber)).Id;
      }
      catch (Exception ex)
      {
        throw new ArgumentException(
          $"Cannot find a Referral that has a Patient with an NHS Number of {nhsNumber} in Referrals", ex);
      }
    }

    protected ReferralStatus GetReferralStatus(int id)
    {
      try
      {
        return Context.ReferralStatuses.Single(referralStatus => referralStatus.Id == id);
      }
      catch (Exception ex)
      {
        throw new ArgumentException(
          $"Cannot find a Referral Status with an id of {id} in ReferralStatuses", ex);
      }
    }

    protected int GetSection12ApprovalStatusIdBySection12ApprovalStatusName(string section12ApprovalStatusName)
    {
      try
      {
        return Context
          .Section12ApprovalStatuses
            .Single(section12ApprovalStatus => section12ApprovalStatus.Name == section12ApprovalStatusName).Id;
      }
      catch (Exception ex)
      {
        throw new ArgumentException(
          $"Cannot find Section 12 Approval Status with the name of {section12ApprovalStatusName} in Section12ApprovalStatuses", ex);
      }
    }

    protected Section12ApprovalStatus GetSection12ApprovalStatusById(int id)
    {
      try
      {
        return Context.Section12ApprovalStatuses
                       .Single(s => s.Id == id);
      }
      catch (Exception ex)
      {
        throw new ArgumentException(
          $"Cannot find Section 12 Approval Status with an id of {id} in Section12ApprovalStatuses", ex);
      }
    }

    protected Section12ApprovalStatus GetSection12ApprovalStatusApproved()
    {
      return GetSection12ApprovalStatusById(Models.Section12ApprovalStatus.APPROVED);
    }


    protected Speciality GetSpeciality(int id)
    {
      try
      {
        return Context.Specialities
            .Single(speciality => speciality.Id == id);
      }
      catch (Exception ex)
      {
        throw new ArgumentException(
          $"Cannot find a Speciality with and Id of {id} in Speciality", ex);
      }
    }

    protected User GetSystemAdminUser()
    {
      if (_systemAdminUser == null)
      {
        _systemAdminUser = GetUserByIdentifier(UserSeeder.IDENTITY_SERVER_IDENTIFIER_SYSTEM_ADMIN);
      }
      return _systemAdminUser;
    }

    protected User GetUserByDisplayName(string displayName)
    {
      try
      {
        return Context.Users
                       .Single(user => user.DisplayName == displayName);
      }
      catch (Exception ex)
      {
        throw new ArgumentException(
          $"Cannot find a user with the Display Name {displayName} in Users", ex);
      }
    }

    protected User GetUserByIdentifier(string identifier)
    {
      try
      {
        return Context.Users
                       .Single(user => user.IdentityServerIdentifier == identifier);
      }
      catch (Exception ex)
      {
        throw new ArgumentException(
          $"Cannot find a user with the Identity Server Identifier {identifier} in Users", ex);
      }
    }
    protected void PopulateActiveAndModifiedWithSystemUser(BaseEntity entity)
    {
      entity.IsActive = true;
      entity.ModifiedAt = _now;
      entity.ModifiedByUser = GetSystemAdminUser();
    }

    protected void PopulateNameDescriptionAndActiveAndModifiedWithSystemUser(
      TEntity entity, string name, string description)
    {
      (entity as NameDescription).Name = name;
      (entity as NameDescription).Description = description;
      PopulateActiveAndModifiedWithSystemUser(entity);
    }

  }
}