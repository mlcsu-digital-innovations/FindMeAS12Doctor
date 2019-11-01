using System;

namespace Fmas12d.Api.ViewModels
{
  public class ReferralViewSummary
  {
    public ReferralViewSummary() {}
    public ReferralViewSummary(Business.Models.Referral model)
    {
      if (model == null) return;

      CreatedAt = model.CreatedAt;
      DefaultToBeCompletedBy = model.DefaultToBeCompletedBy;
      Id = model.Id;
      LeadAmhpUser = new UserSummary(model.LeadAmhpUser.DisplayName, model.LeadAmhpUserId);
      Patient = new PatientSummary(model);
      StatusName = model.StatusName;
    }

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? DefaultToBeCompletedBy { get; set; }
    public int Id { get; set; }
    public UserSummary LeadAmhpUser { get; set; }
    public PatientSummary Patient { get; set; }
    public string StatusName { get; set; }
  }
}