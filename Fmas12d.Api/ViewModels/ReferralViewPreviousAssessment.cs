using System;
using System.Collections.Generic;

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

      AmhpUserName = model.AmhpUser?.DisplayName;
      DoctorNamesAllocated = model.DoctorNamesAllocated;
      Id = model.Id;
      Postcode = model.Postcode;
      UnsuccessfulAssessmentTypeName = model.UnsuccessfulAssessmentType?.Name;
    }

    public string AmhpUserName { get; set; }
    public DateTimeOffset? CompletedTime { get; set; }
    public IList<string> DoctorNamesAllocated { get; set; }
    public int Id { get; set; }
    public string Postcode { get; set; }
    public string UnsuccessfulAssessmentTypeName { get; set; }
  }
}