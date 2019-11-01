using System;

namespace Fmas12d.Data.Entities
{
  public interface IReferral
  {
    DateTimeOffset CreatedAt { get; set; }
    int CreatedByUserId { get; set; }
    bool IsPlannedExamination { get; set; }
    int PatientId { get; set; }
    int ReferralStatusId { get; set; }
    int LeadAmhpUserId { get; set; }    
  }
}