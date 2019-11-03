using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Api.ViewModels
{
  public class ReferralView
  {
    public ReferralView() {}
    public ReferralView(Business.Models.Referral model)
    {
      if (model == null) return;

      CurrentAssessment = new ReferralViewCurrentAssessment(model.CurrentAssessment);
      Id = model.Id;
      LeadAmhp = model.LeadAmhp;
      PatientIdentifier = model.PatientIdentifier;
      PreviousAssessments = model.PreviousAssessments
        ?.Select(p => new ReferralViewPreviousAssessment(p)).ToList();
      StatusName = model.StatusName;
    }

    public ReferralViewCurrentAssessment CurrentAssessment { get; set; }
    public int Id { get; set; }
    public string LeadAmhp { get; set; }
    public string PatientIdentifier { get; set; }
    public IEnumerable<ReferralViewPreviousAssessment> PreviousAssessments { get; set; }
    public string StatusName { get; set; }
  }
}