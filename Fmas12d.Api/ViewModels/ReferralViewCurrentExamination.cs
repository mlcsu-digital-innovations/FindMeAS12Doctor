using System;
using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Api.ViewModels
{
  /// <summary>
  /// TODO : Type of Examination
  /// </summary>
  public class ReferralViewCurrentExamination
  {
    public ReferralViewCurrentExamination() {}
    public ReferralViewCurrentExamination(Business.Models.Examination model)
    {
      if (model == null) return;

      AmhpUserName = model.AmhpUser?.DisplayName;
      DetailTypes = model.DetailTypes?.Select(d => new ExaminationDetailType(d)).ToList();
      DoctorNamesAccepted = model.DoctorNamesAccepted;
      DoctorNamesAllocated = model.DoctorNamesAllocated;
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
    public virtual IList<ExaminationDetailType> DetailTypes { get; set; }
    public IList<string> DoctorNamesAccepted { get; set; }
    public IList<string> DoctorNamesAllocated { get; set; }
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