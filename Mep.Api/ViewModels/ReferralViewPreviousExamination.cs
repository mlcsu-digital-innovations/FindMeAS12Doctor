using System;
using System.Collections.Generic;

namespace Mep.Api.ViewModels
{
  /// <summary>
  /// TODO : Type of Examination
  /// </summary>
  public class ReferralViewPreviousExamination
  {
    public string AmhpUserName { get; set; }
    public DateTimeOffset? CompletedTime { get; set; }
    public IList<string> DoctorNamesAllocated { get; set; }
    public int Id { get; set; }
    public string Postcode { get; set; }
    public string UnsuccessfulExaminationTypeName { get; set; }
  }
}