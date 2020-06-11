using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Fmas12d.Api.ViewModels
{
  public class Assessment : BaseViewModel
  {
    public Assessment(Business.Models.Assessment model) {
      
      if (model == null) return;

      Address1 = model.Address1;
      Address2 = model.Address2;
      Address3 = model.Address3;
      Address4 = model.Address4;
      AmhpUser = new User(model.AmhpUser);
      // AmhpUserId = entity.AmhpUserId;
      Ccg = new Ccg(model.Ccg);
      CcgId = model.CcgId;
      CompletedByUser = new User(model.CompletedByUser);
      CompletedByUserId = model.CompletedByUserId;
      CompletedTime = model.CompletedTime;
      CompletionConfirmationByUser = new User(model.CompletionConfirmationByUser);
      CompletionConfirmationByUserId = model.CompletionConfirmationByUserId;
      CreatedByUser = new User(model.CreatedByUser);
      CreatedByUserId = model.CreatedByUserId;
      // Details = entity.Details?.Select(d => new AssessmentDetail(d)).ToList();
      // Doctors = entity.Doctors?.Select(d => new AssessmentDoctor(d)).ToList();
      Id = model.Id;
      IsActive = model.IsActive;
      IsSuccessful = model.IsSuccessful;
      // Latitude = entity.Latitude;
      // Longitude = entity.Longitude;
      MeetingArrangementComment = model.MeetingArrangementComment;
      // MustBeCompletedBy = entity.MustBeCompletedBy;
      // TODO NonPaymentLocation = null;
      NonPaymentLocationId = model.NonPaymentLocationId;
      Postcode = model.Postcode;
      // PreferredDoctorGenderType = new GenderType(entity.PreferredDoctorGenderType);
      PreferredDoctorGenderTypeId = model.PreferredDoctorGenderTypeId;
      // if (!ignoreReferral)
      // {
      //   Referral = new Referral(entity.Referral, false);
      // }
      ReferralId = model.ReferralId;
      ScheduledTime = model.ScheduledTime;
      // Speciality = new Speciality(entity.Speciality);
      // SpecialityId = entity.SpecialityId;
      UnsuccessfulAssessmentType = new UnsuccessfulAssessmentType(model.UnsuccessfulAssessmentType);
      UnsuccessfulAssessmentTypeId = model.UnsuccessfulAssessmentTypeId;
      //TODO UserAssessmentClaims = null;
      // UserAssessmentNotifications = entity
      //   .UserAssessmentNotifications
      //   ?.Select(u => new UserAssessmentNotification(u)).ToList();
    }

    [Required]
    [MaxLength(200)]
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Address4 { get; set; }
    public virtual User AmhpUser { get; set; }
    public virtual Ccg Ccg { get; set; }
    public int? CcgId { get; set; }
    public int? CompletedByUserId { get; set; }
    public virtual User CompletedByUser { get; set; }
    public DateTimeOffset? CompletedTime { get; set; }
    public int? CompletionConfirmationByUserId { get; set; }
    public virtual User CompletionConfirmationByUser { get; set; }
    public virtual User CreatedByUser { get; set; }
    public int CreatedByUserId { get; set; }
    public virtual IList<AssessmentDetailType> DetailTypes { get; set; }
    public Boolean HasBeenCompleted {
      get {
        return CompletedByUserId.HasValue;
      }
    }
    public Boolean HasBeenReviewed {
      get {
        return CompletionConfirmationByUserId.HasValue;
      }
    }
    public bool? IsCompleted {
      get {
        return CompletedByUser != null;
      }
    }
    public bool? IsSuccessful { get; set; }
    [MaxLength(2000)]
    public string MeetingArrangementComment { get; set; }
    public DateTimeOffset MustBeCompletedBy { get; set; }
    public int? NonPaymentLocationId { get; set; }
    public virtual NonPaymentLocation NonPaymentLocation { get; set; }
    [Required]
    [MaxLength(10)]
    public string Postcode { get; set; }
    public int ReferralId { get; set; }
    public virtual Referral Referral { get; set; }
    public DateTimeOffset? ScheduledTime { get; set; }
    public int SpecialityId { get; set; }
    public virtual Speciality Speciality { get; set; }
    public int? UnsuccessfulAssessmentTypeId { get; set; }
    public UnsuccessfulAssessmentType UnsuccessfulAssessmentType { get; set; }
    public virtual IList<UserAssessmentClaim> UserAssessmentClaims { get; set; }
    public virtual IList<UserAssessmentNotification> UserAssessmentNotifications { get; set; }
    public virtual GenderType PreferredDoctorGenderType { get; set; }
    public int? PreferredDoctorGenderTypeId { get; set; }
  }
}