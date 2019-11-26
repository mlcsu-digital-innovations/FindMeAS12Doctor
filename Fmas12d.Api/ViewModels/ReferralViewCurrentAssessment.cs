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

      AmhpUserName = model.AmhpUser?.DisplayName;
      DetailTypes = model.DetailTypes?.Select(d => new AssessmentDetailType(d)).ToList();
      DoctorsSelected = model.DoctorsSelected?.Select(d => new AssessmentViewDoctor(d)).ToList();
      DoctorsAllocated = model.DoctorsAllocated?.Select(d => new AssessmentViewDoctor(d)).ToList();
      FullAddress = model.FullAddress;
      Id = model.Id;
      IsPlanned = model.IsPlanned;
      MeetingArrangementComment = model.MeetingArrangementComment;
      MustBeCompletedBy = model.MustBeCompletedBy;
      Postcode = model.Postcode;
      PreferredDoctorGenderType = new IdNameDescription(model.PreferredDoctorGenderType);
      ScheduledTime = model.ScheduledTime;
      Speciality = new IdNameDescription(model.Speciality);
    }

    public string AmhpUserName { get; set; }
    public virtual IList<AssessmentDetailType> DetailTypes { get; set; }
    public IList<AssessmentViewDoctor> DoctorsSelected { get; set; }
    public IList<AssessmentViewDoctor> DoctorsAllocated { get; set; }
    public string FullAddress { get; set; }
    public int Id { get; set; }
    public bool IsPlanned { get; set; }
    public string MeetingArrangementComment { get; set; }
    public DateTimeOffset? MustBeCompletedBy { get; set; }
    public string Postcode { get; set; }
    public IdNameDescription PreferredDoctorGenderType { get; set; }
    public DateTimeOffset? ScheduledTime { get; set; }
    public IdNameDescription Speciality { get; set; }
  }
}