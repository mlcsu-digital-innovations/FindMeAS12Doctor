using System;
using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Api.ViewModels
{
  /// <summary>
  /// TODO : Type of Assessment
  /// </summary>
  public class ReferralViewPreviousAssessment
  {
    public ReferralViewPreviousAssessment(Business.Models.Assessment model)
    {
      if (model == null) return;

      AmhpUserName = model.AmhpUserName;
      DoctorAttendedNames =
        model.DoctorsAttended?.Select(d => d.DoctorUserDisplayName).ToList();
      Id = model.Id;
      IsSuccessful = model.IsSuccessful ?? false;
      LeadAmhpUserName = model.LeadAmhpName;
      Postcode = model.Postcode;
      ScheduledTime = model.ScheduledTime;
      UnsuccessfulAssessmentTypeName = model.UnsuccessfulAssessmentTypeName;
    }

    public string AmhpUserName { get; set; }
    public IList<string> DoctorAttendedNames { get; set; }
    public int Id { get; set; }
    public bool IsSuccessful { get; set; }
    public string LeadAmhpUserName { get; set; }
    public string Postcode { get; set; }
    public DateTimeOffset? ScheduledTime { get; set; }
    public string Status { get { return IsSuccessful ? "Successful" : "Unsuccessful"; } }
    public string UnsuccessfulAssessmentTypeName { get; set; }
  }
}