using System;
using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Api.ViewModels
{
  /// <summary>
  /// TODO : Type of Assessment
  /// </summary>
  public class ReferralViewCurrentAssessment
  {
    public ReferralViewCurrentAssessment() {}
    public ReferralViewCurrentAssessment(Business.Models.Assessment model)
    {
      if (model == null) return;

      AmhpUser = new UserSummary(model.AmhpUser);
      CompletedAt = model.CompletedTime;
      DetailTypes = model.DetailTypes?.Select(d => new AssessmentDetailType(d)).ToList();
      DoctorsAllocated = model.DoctorsAllocated?.Select(d => new AssessmentViewDoctor(d)).ToList();
      DoctorsAttended = model.DoctorsAttended?.Select(d => new AssessmentViewDoctor(d)).ToList();
      DoctorsSelected = model.DoctorsSelected?.Select(d => new AssessmentViewDoctor(d)).ToList();
      FullAddress = model.FullAddress;
      Id = model.Id;
      IsPlanned = model.IsPlanned;
      IsSuccessful = model.IsSuccessful;
      MeetingArrangementComment = model.MeetingArrangementComment;
      MustBeCompletedBy = model.MustBeCompletedBy;
      Postcode = model.Postcode;
      PreferredDoctorGenderType = new IdNameDescription(model.PreferredDoctorGenderType);
      ScheduledTime = model.ScheduledTime;
      Speciality = new IdNameDescription(model.Speciality);
      UnsuccessfulAssessmentTypeName = model.UnsuccessfulAssessmentType?.Name;
    }

    public UserSummary AmhpUser { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
    public virtual IList<AssessmentDetailType> DetailTypes { get; set; }
    public IList<AssessmentViewDoctor> DoctorsAttended { get; set; }
    public IList<AssessmentViewDoctor> DoctorsSelected { get; set; }
    public IList<AssessmentViewDoctor> DoctorsAllocated { get; set; }
    public string FullAddress { get; set; }
    public bool HasOutcome { 
      get
      {
        return IsSuccessful.HasValue;
      } 
    }
    public int Id { get; set; }
    public bool IsPlanned { get; set; }
    public bool? IsSuccessful { get; set; }
    public string MeetingArrangementComment { get; set; }
    public DateTimeOffset? MustBeCompletedBy { get; set; }
    public string Postcode { get; set; }
    public IdNameDescription PreferredDoctorGenderType { get; set; }
    public DateTimeOffset? ScheduledTime { get; set; }
    public IdNameDescription Speciality { get; set; }
    public string UnsuccessfulAssessmentTypeName { get; set; }
  }
}