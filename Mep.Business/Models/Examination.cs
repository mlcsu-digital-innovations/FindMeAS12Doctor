using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Mep.Data.Entities;

namespace Mep.Business.Models
{
  public class Examination : BaseModel
  {
    public Examination() { }
    public Examination(Data.Entities.Examination entity)
    {
      Address1 = entity.Address1;
      Address2 = entity.Address2;
      Address3 = entity.Address3;
      Address4 = entity.Address4;
      AmhpUserId = entity.AmhpUserId;
      Ccg = entity.Ccg == null ? null : new Ccg(entity.Ccg);
      CcgId = entity.CcgId;
      //TODO CompletedByUser = null;
      CompletedByUserId = entity.CompletedByUserId;
      CompletedTime = entity.CompletedTime;
      CompletionConfirmationByUser = null;
      CompletionConfirmationByUserId = entity.CompletionConfirmationByUserId;
      //TODO CreatedByUser = null;
      CreatedByUserId = entity.CreatedByUserId;
      //TODO Details = null;
      Doctors = entity.Doctors?.Select(d => new ExaminationDoctor(d)).ToList();
      Id = entity.Id;
      IsActive = entity.IsActive;
      IsSuccessful = entity.IsSuccessful;
      MeetingArrangementComment = entity.MeetingArrangementComment;
      MustBeCompletedBy = entity.MustBeCompletedBy;
      //TODO NonPaymentLocation = null;
      NonPaymentLocationId = entity.NonPaymentLocationId;
      Postcode = entity.Postcode;
      //TODO PreferredDoctorGenderType = null;
      PreferredDoctorGenderTypeId = entity.PreferredDoctorGenderTypeId;
      Referral = entity.Referral == null ? null : new Referral(entity.Referral);
      ReferralId = entity.ReferralId;
      ScheduledTime = entity.ScheduledTime;
      //TODO Speciality = null;
      SpecialityId = entity.SpecialityId;
      //TODO UnsuccessfulExaminationType = null;
      UnsuccessfulExaminationTypeId = entity.UnsuccessfulExaminationTypeId;
      //TODO UserExaminationClaims = null;
      //TODO UserExaminationNotifications = null;
    }

    [Required]
    [MaxLength(200)]
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Address4 { get; set; }
    public User AmhpUser { get; set; }
    public int AmhpUserId { get; set; }
    public virtual Ccg Ccg { get; set; }
    public int? CcgId { get; set; }
    public int? CompletedByUserId { get; set; }
    public virtual User CompletedByUser { get; set; }
    public DateTimeOffset? CompletedTime { get; set; }
    public int? CompletionConfirmationByUserId { get; set; }
    public virtual User CompletionConfirmationByUser { get; set; }
    public virtual User CreatedByUser { get; set; }
    public int CreatedByUserId { get; set; }
    public IList<ExaminationDoctor> Doctors { get; set; }
    public virtual IList<int> DetailTypeIds { get; set; }
    public virtual IList<ExaminationDetail> Details { get; set; }
    public bool? IsSuccessful { get; set; }
    [MaxLength(2000)]
    public string MeetingArrangementComment { get; set; }
    public DateTimeOffset? MustBeCompletedBy { get; set; }
    public int? NonPaymentLocationId { get; set; }
    public virtual NonPaymentLocation NonPaymentLocation { get; set; }
    [Required]
    [MaxLength(10)]
    public string Postcode { get; set; }
    public virtual GenderType PreferredDoctorGenderType { get; set; }
    public int? PreferredDoctorGenderTypeId { get; set; }
    public int ReferralId { get; set; }
    public virtual Referral Referral { get; set; }
    public DateTimeOffset? ScheduledTime { get; set; }
    public int? SpecialityId { get; set; }
    public Speciality Speciality { get; set; }
    public int? UnsuccessfulExaminationTypeId { get; set; }
    public UnsuccessfulExaminationType UnsuccessfulExaminationType { get; set; }
    public virtual IList<UserExaminationClaim> UserExaminationClaims { get; set; }
    public virtual IList<UserExaminationNotification> UserExaminationNotifications { get; set; }

    public string AmhpUserName
    {
      get
      {
        return UserExaminationNotifications
          .Where(u => u.IsActive)
          .FirstOrDefault(u => u.IsAmhp)
          ?.UserName;
      }
    }

    public DateTimeOffset DateTime
    { get { return (DateTimeOffset)(MustBeCompletedBy ?? ScheduledTime); } }

    public virtual IList<ExaminationDetailType> DetailTypes
    {
      get
      {
        return Details.Where(d => d.IsActive)
                      .Select(d => d.ExaminationDetailType).ToList();
      }
    }

    public IList<string> DoctorNamesAccepted
    {
      get
      {
        return Doctors
          .Where(d => d.IsActive)
          .Where(d => d.StatusId == ExaminationDoctorStatus.SELECTED)
          .Select(d => d.DoctorUser?.DisplayName)
          .ToList();
      }
    }

    public IList<string> DoctorNamesAllocated
    {
      get
      {
        return DoctorsAllocated?.Select(d => d.DisplayName)
                               ?.ToList();
      }
    }

    public IList<User> DoctorsAllocated
    {
      get
      {
        if (Doctors == null)
        {
          return null;
        }
        else
        {
          return Doctors
            .Where(d => d.IsActive)
            .Where(d => d.StatusId == ExaminationDoctorStatus.ALLOCATED)
            .Select(u => u.DoctorUser)
            .ToList();
        }
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

    public bool HasDetailTypeIds
    { get { return DetailTypeIds != null && DetailTypeIds.Count > 0; } }

    public bool IsCurrent
    {
      get
      {
        return IsActive &&
               UnsuccessfulExaminationTypeId == null &&
               CompletionConfirmationByUserId == null;
      }
    }

    public bool IsPlanned
    { get { return ScheduledTime != null; } }

    public string PatientIdentifier
    { get { return Referral?.PatientIdentifier; } }

    public string PreferredDoctorGenderTypeName
    { get { return PreferredDoctorGenderType?.Name; } }

    public string SpecialityName
    { get { return Speciality?.Name; } }

    public string UnsuccessfulExaminationTypeName
    { get { return UnsuccessfulExaminationType?.Name; } }

    // Need EF core 3.1 fix: https://github.com/aspnet/EntityFrameworkCore/issues/18127
    // public static Expression<Func<Data.Entities.Examination, Examination>> ProjectFromEntity
    // {
    //   get
    //   {
    //     return e => new Examination()
    //     {         
    //     };
    //   }
    // }
  }
}