using System;
using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface IReferral
  {
    DateTimeOffset CreatedAt { get; set; }
    int CreatedByUserId { get; set; }
    int PatientId { get; set; }
    int ReferralStatusId { get; set; }
  }
}