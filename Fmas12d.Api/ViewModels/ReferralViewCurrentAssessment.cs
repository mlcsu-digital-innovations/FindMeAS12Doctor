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
      DoctorsSelected = model.DoctorNamesAccepted?.Select(d => new AssessmentViewDoctor(d)).ToList();
      DoctorsAllocated = model.DoctorNamesAllocated?.Select(d => new AssessmentViewDoctor(d)).ToList();
      FullAddress = model.FullAddress;
      Id = model.Id;
      IsPlanned = model.IsPlanned;
      MeetingArrangementComment = model.MeetingArrangementComment;
      MustBeCompletedBy = model.MustBeCompletedBy;
      Postcode = model.Postcode;
      PreferredDoctorGenderTypeName = model.PreferredDoctorGenderType?.Name;
      ScheduledTime = model.ScheduledTime;
      SpecialityName = model.Speciality?.Name;
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
    public string PreferredDoctorGenderTypeName { get; set; }
    public DateTimeOffset? ScheduledTime { get; set; }
    public string SpecialityName { get; set; }
  }
}