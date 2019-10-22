using System.Collections.Generic;

namespace Mep.Api.ViewModels
{
  public class ReferralView
  {
    public ReferralViewCurrentExamination CurrentExamination { get; set; }
    public int Id { get; set; }
    public string LeadAmhp { get; set; }
    public string PatientIdentifier { get; set; }
    public IEnumerable<ReferralViewPreviousExamination> PreviousExaminations { get; set; }
    public string Status { get; set; }
  }
}