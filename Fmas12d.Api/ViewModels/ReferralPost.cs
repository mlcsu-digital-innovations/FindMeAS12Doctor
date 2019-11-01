using System;

namespace Mep.Api.ViewModels
{
  public class ReferralPost
  {
    public ReferralPost(Business.Models.Referral model)
    {
      if (model == null) return;

      CreatedAt = model.CreatedAt;
      CreatedByUserId = model.CreatedByUserId;
      Id = model.Id;
      LeadAmhpUserId = model.LeadAmhpUserId;
      PatientId = model.PatientId;
    }

    public DateTimeOffset CreatedAt { get; set; }
    public int CreatedByUserId { get; set; }
    public int Id { get; set; }
    public int LeadAmhpUserId { get; set; }
    public int PatientId { get; set; }
  }
}