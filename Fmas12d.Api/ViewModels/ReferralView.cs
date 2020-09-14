using System.Collections.Generic;
using System.Linq;
using System;

namespace Fmas12d.Api.ViewModels
{
  public class ReferralView
  {
    public ReferralView() {}
    public ReferralView(Business.Models.Referral model)
    {
      if (model == null) return;

      CreatedAt = model.CreatedAt;
      CurrentAssessment = new ReferralViewCurrentAssessment(model.CurrentAssessment);
      Id = model.Id;
      LeadAmhp = model.LeadAmhpName;
      PatientIdentifier = model.PatientIdentifier;
      PreviousAssessments = model.PreviousAssessments
        ?.Select(p => new ReferralViewPreviousAssessment(p)).ToList();
      ReferralStatusId = model.ReferralStatusId;
      StatusName = model.StatusName;
      
    }

    public DateTimeOffset CreatedAt { get; set; }
    public ReferralViewCurrentAssessment CurrentAssessment { get; set; }
    public int Id { get; set; }
    public string LeadAmhp { get; set; }
    public string PatientIdentifier { get; set; }
    public IEnumerable<ReferralViewPreviousAssessment> PreviousAssessments { get; set; }
    public int ReferralStatusId { get; set; }
    public string StatusName { get; set; }
  }
}