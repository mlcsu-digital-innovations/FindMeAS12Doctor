using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Models
{
  public class Assessment : BaseModel
  {
    public Assessment() { }
    public Assessment(Data.Entities.Assessment entity, bool ignoreReferral = false)
      : base(entity)
    {
      if (entity == null) return;

      Address1 = entity.Address1;
      Address2 = entity.Address2;
      Address3 = entity.Address3;
      Address4 = entity.Address4;
      AmhpUser = new User(entity.AmhpUser);
      AmhpUserId = entity.AmhpUserId;
      Ccg = new Ccg(entity.Ccg);
      CcgId = entity.CcgId;
      CompletedByUser = new User(entity.CompletedByUser);
      CompletedByUserId = entity.CompletedByUserId;
      CompletedTime = entity.CompletedTime;
      CompletionConfirmationByUser = new User(entity.CompletionConfirmationByUser);
      CompletionConfirmationByUserId = entity.CompletionConfirmationByUserId;
      CreatedByUser = new User(entity.CreatedByUser);
      CreatedByUserId = entity.CreatedByUserId;
      Details = entity.Details?.Select(d => new AssessmentDetail(d)).ToList();
      Doctors = entity.Doctors?.Select(d => new AssessmentDoctor(d)).ToList();
      Id = entity.Id;
      IsActive = entity.IsActive;
      IsSuccessful = entity.IsSuccessful;
      Latitude = entity.Latitude;
      Longitude = entity.Longitude;
      MeetingArrangementComment = entity.MeetingArrangementComment;
      MustBeCompletedBy = entity.MustBeCompletedBy;
      // TODO NonPaymentLocation = null;
      NonPaymentLocationId = entity.NonPaymentLocationId;
      Postcode = entity.Postcode;
      PreferredDoctorGenderType = new GenderType(entity.PreferredDoctorGenderType);
      PreferredDoctorGenderTypeId = entity.PreferredDoctorGenderTypeId;
      if (!ignoreReferral)
      {
        Referral = new Referral(entity.Referral, false);
      }
      ReferralId = entity.ReferralId;
      ScheduledTime = entity.ScheduledTime;
      Speciality = new Speciality(entity.Speciality);
      SpecialityId = entity.SpecialityId;
      //TODO UnsuccessfulAssessmentType = null;
      UnsuccessfulAssessmentTypeId = entity.UnsuccessfulAssessmentTypeId;
      //TODO UserAssessmentClaims = null;
      UserAssessmentNotifications = entity
        .UserAssessmentNotifications
        ?.Select(u => new UserAssessmentNotification(u)).ToList();
    }

    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Address4 { get; set; }
    public User AmhpUser { get; set; }
    public int AmhpUserId { get; set; }
    public IEnumerable<IUserAvailabilityDoctor> AvailableDoctors { get; set; }
    public virtual Ccg Ccg { get; set; }
    public int? CcgId { get; set; }
    public int? CompletedByUserId { get; set; }
    public virtual User CompletedByUser { get; set; }
    public DateTimeOffset? CompletedTime { get; set; }
    public int? CompletionConfirmationByUserId { get; set; }
    public virtual User CompletionConfirmationByUser { get; set; }
    public virtual User CreatedByUser { get; set; }
    public int CreatedByUserId { get; set; }
    public IList<AssessmentDoctor> Doctors { get; set; }    
    public virtual IList<int> DetailTypeIds { get; set; }
    public virtual IList<AssessmentDetail> Details { get; set; }
    public bool? IsSuccessful { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }    
    public string MeetingArrangementComment { get; set; }
    public DateTimeOffset? MustBeCompletedBy { get; set; }
    public int? NonPaymentLocationId { get; set; }
    public virtual NonPaymentLocation NonPaymentLocation { get; set; }
    public string Postcode { get; set; }
    public virtual GenderType PreferredDoctorGenderType { get; set; }
    public int? PreferredDoctorGenderTypeId { get; set; }
    public int ReferralId { get; set; }
    public virtual Referral Referral { get; set; }
    public DateTimeOffset? ScheduledTime { get; set; }
    public int? SpecialityId { get; set; }
    public Speciality Speciality { get; set; }
    public int? UnsuccessfulAssessmentTypeId { get; set; }
    public UnsuccessfulAssessmentType UnsuccessfulAssessmentType { get; set; }
    public virtual IList<UserAssessmentClaim> UserAssessmentClaims { get; set; }
    public virtual IList<UserAssessmentNotification> UserAssessmentNotifications { get; set; }

    public string AmhpUserName { get { return AmhpUser?.DisplayName; } }

    public DateTimeOffset DateTime
    { get { return ScheduledTime ?? MustBeCompletedBy ?? default; } }

    public virtual IList<AssessmentDetailType> DetailTypes
    {
      get
      {
        return Details?.Where(d => d.IsActive)
                       .Select(d => d.AssessmentDetailType)
                       .ToList();
      }
    }

    public IList<AssessmentDoctor> DoctorsAllocated
    {
      get
      {
        return GetAssessmentActiveDoctorsByStatus(AssessmentDoctorStatus.ALLOCATED);
      }
    }

    public IList<AssessmentDoctor> DoctorsSelected
    {
      get
      {
        return GetAssessmentActiveDoctorsByStatus(AssessmentDoctorStatus.SELECTED);
      }
    }

    public string FullAddress
    {
      get
      {
        StringBuilder fullAddress = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(Address1)) { fullAddress.Append(Address1); }
        if (!string.IsNullOrWhiteSpace(Address2)) { fullAddress.Append(", " + Address2); }
        if (!string.IsNullOrWhiteSpace(Address3)) { fullAddress.Append(", " + Address3); }
        if (!string.IsNullOrWhiteSpace(Address4)) { fullAddress.Append(", " + Address4); }
        return fullAddress.ToString();
      }
    }

    private IList<AssessmentDoctor> GetAssessmentActiveDoctorsByStatus(
      int assessmentDoctorStatusId
    )
    {
      if (Doctors == null)
      {
        return null;
      }
      else
      {
        return Doctors
          .Where(d => d.IsActive)
          .Where(d => d.StatusId == assessmentDoctorStatusId)
          .ToList();
      }
    }

    public bool HasDetailTypeIds
    { get { return DetailTypeIds != null && DetailTypeIds.Count > 0; } }

    public bool IsCurrent
    {
      get
      {
        return IsActive &&
               UnsuccessfulAssessmentTypeId == null &&
               CompletionConfirmationByUserId == null;
      }
    }

    public bool IsPlanned
    { get { return ScheduledTime != null; } }

    public string LeadAmhpName { get { return Referral?.LeadAmhpName; } }

    public string PatientIdentifier
    { get { return Referral?.PatientIdentifier; } }

    public string PreferredDoctorGenderTypeName
    { get { return PreferredDoctorGenderType?.Name; } }

    public string SpecialityName
    { get { return Speciality?.Name; } }

    public string UnsuccessfulAssessmentTypeName
    { get { return UnsuccessfulAssessmentType?.Name; } }

    // Need EF core 3.1 fix: https://github.com/aspnet/EntityFrameworkCore/issues/18127
    // public static Expression<Func<Data.Entities.Assessment, Assessment>> ProjectFromEntity
    // {
    //   get
    //   {
    //     return e => new Assessment()
    //     {         
    //     };
    //   }
    // }
  }
}