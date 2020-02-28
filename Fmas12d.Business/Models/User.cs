using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Fmas12d.Business.Models
{
  public class User : BaseModel, IUser
  {
    public User() { }
    public User(Data.Entities.User entity) : base(entity)
    {
      if (entity == null) return;
      // TODO AmhpReferrals
      // TODO BankDetails
      BankDetails = entity.BankDetails?.Select(bd => new BankDetail(bd)).ToList();
      // TODO CompletedAssessments
      // TODO CompletionConfirmationAssessments
      ContactDetails = entity.ContactDetails?.Select(cd => new ContactDetail(cd, false)).ToList();
      // TODO CreatedAssessments
      DisplayName = entity.DisplayName;
      // TODO DoctorStatuses
      GenderType = new GenderType(entity.GenderType);
      GenderTypeId = entity.GenderTypeId;
      GmcNumber = entity.GmcNumber;
      HasReadTermsAndConditions = entity.HasReadTermsAndConditions;
      IdentityServerIdentifier = entity.IdentityServerIdentifier;
      // TODO OnCallUsers
      // TODO Organisation
      OrganisationId = entity.OrganisationId;
      // TODO PaymentMethods
      ProfileType = new ProfileType(entity.ProfileType);
      ProfileTypeId = entity.ProfileTypeId;
      // TODO Referrals
      Section12ApprovalStatus = new Section12ApprovalStatus(entity.Section12ApprovalStatus);
      Section12ApprovalStatusId = entity.Section12ApprovalStatusId;
      Section12ExpiryDate = entity.Section12ExpiryDate;
      UserSpecialities = entity
        .UserSpecialities
        ?.Select(us => new UserSpeciality(us, false))
        .ToList();
      // TODO UserAssessmentClaims
      // TODO UserAssessmentClaimSelections
      // TODO UserAssessmentNotification
    }

    public virtual IList<Referral> AmhpReferrals { get; set; }
    public virtual IList<BankDetail> BankDetails { get; set; }
    public virtual IList<Assessment> CompletedAssessments { get; set; }
    public virtual IList<Assessment> CompletionConfirmationAssessments { get; set; }
    public virtual IList<ContactDetail> ContactDetails { get; set; }
    public virtual IList<Assessment> CreatedAssessments { get; set; }
    [MaxLength(256)]
    public string DisplayName { get; set; }
    public virtual IList<UserAvailability> UserAvailabilities { get; set; }
    public virtual GenderType GenderType { get; set; }
    public int? GenderTypeId { get; set; }
    public int? GmcNumber { get; set; }
    public bool HasReadTermsAndConditions { get; set; }
    [MaxLength(50)]
    [Required]
    public string IdentityServerIdentifier { get; set; }
    public virtual Organisation Organisation { get; set; }
    public int OrganisationId { get; set; }
    public virtual IList<PaymentMethod> PaymentMethods { get; set; }
    public virtual ProfileType ProfileType { get; set; }
    public int ProfileTypeId { get; set; }
    public virtual IList<Referral> Referrals { get; set; }
    public virtual Section12ApprovalStatus Section12ApprovalStatus { get; set; }
    public int? Section12ApprovalStatusId { get; set; }
    public DateTimeOffset? Section12ExpiryDate { get; set; }
    public virtual IList<UserSpeciality> UserSpecialities { get; set; }
    public virtual IList<UserAssessmentClaim> UserAssessmentClaims { get; set; }
    public virtual IList<UserAssessmentClaim> UserAssessmentClaimSelections { get; set; }
    public virtual IList<UserAssessmentNotification> UserAssessmentNotifications { get; set; }

    public string FcmToken { get; set; }

    public string GenderName { get { return GenderType?.Name; } }

    public bool IsAmhp
    {
      get
      {
        if (ProfileType == null)
        {
          return ProfileType.IsIdAnAmhp(ProfileTypeId);
        }
        else
        {
          return ProfileType.IsAmhp;
        }
      }
    }

    public bool IsDoctor
    {
      get
      {
        if (ProfileType == null)
        {
          return ProfileType.IsIdADoctor(ProfileTypeId);
        }
        else
        {
          return ProfileType.IsDoctor;
        }
      }
    }

    public bool IsFinance
    {
      get
      {
        if (ProfileType == null)
        {
          return ProfileType.IsIdFinance(ProfileTypeId);
        }
        else
        {
          return ProfileType.IsFinance;
        }
      }
    }

    public string ProfileTypeName { get { return ProfileType?.Name; } }

    // Need EF core 3.1 fix: https://github.com/aspnet/EntityFrameworkCore/issues/18127
    // for this to work with .ThenInclude()
    public static Expression<Func<Data.Entities.User, User>> ProjectFromEntity
    {
      get
      {
        return userEntity => new User(userEntity);
      }
    }

    public ContactDetail GetContactDetailTypeBase()
    {
      return ContactDetails?
        .SingleOrDefault(cd => cd.ContactDetailTypeId == ContactDetailType.BASE);
    }

    internal Data.Entities.User MapToEntity()
    {
      Data.Entities.User entity = new Data.Entities.User()
      {
        DisplayName = DisplayName,
        GenderTypeId = GenderTypeId,
        GmcNumber = GmcNumber,
        IdentityServerIdentifier = IdentityServerIdentifier,
        OrganisationId = OrganisationId,
        ProfileTypeId = ProfileTypeId
      };

      BaseMapToEntity(entity);
      return entity;
    }

  }
}