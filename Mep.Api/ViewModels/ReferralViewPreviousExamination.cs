using System;
using System.Collections.Generic;

namespace Mep.Api.ViewModels
{
  public class ReferralViewPreviousExamination
  {
    public string AmhpUserName { get; set; }
    public DateTimeOffset? CompletedTime { get; set; }
    public IList<string> DoctorNamesAllocated { get; set; }
    public int Id { get; set; }
    public string Postcode { get; set; }
    //TODO : Type of Examination
    public string UnsuccessfulExaminationTypeName { get; set; }
  }
}