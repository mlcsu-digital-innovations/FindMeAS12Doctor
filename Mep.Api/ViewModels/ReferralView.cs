using System.Collections.Generic;
using System.Linq;

namespace Mep.Api.ViewModels
{
  public class ReferralView
  {
    public ReferralView() {}
    public ReferralView(Business.Models.Referral model)
    {
      if (model == null) return;

      CurrentExamination = new ReferralViewCurrentExamination(model.CurrentExamination);
      Id = model.Id;
      LeadAmhp = model.LeadAmhp;
      PatientIdentifier = model.PatientIdentifier;
      PreviousExaminations = model.PreviousExaminations
        ?.Select(p => new ReferralViewPreviousExamination(p)).ToList();
      StatusName = model.StatusName;
    }

    public ReferralViewCurrentExamination CurrentExamination { get; set; }
    public int Id { get; set; }
    public string LeadAmhp { get; set; }
    public string PatientIdentifier { get; set; }
    public IEnumerable<ReferralViewPreviousExamination> PreviousExaminations { get; set; }
    public string StatusName { get; set; }
  }
}