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

      AmhpUserName = model.AmhpUser?.DisplayName;
      DoctorsAllocated = model.DoctorsAllocated?.Select(d => new AssessmentViewDoctor(d)).ToList();
      Id = model.Id;
      Postcode = model.Postcode;
      UnsuccessfulAssessmentTypeName = model.UnsuccessfulAssessmentType?.Name;
    }

    public string AmhpUserName { get; set; }
    public DateTimeOffset? CompletedTime { get; set; }
    public IList<AssessmentViewDoctor> DoctorsAllocated { get; set; }
    public int Id { get; set; }
    public string Postcode { get; set; }
    public string UnsuccessfulAssessmentTypeName { get; set; }
  }
}