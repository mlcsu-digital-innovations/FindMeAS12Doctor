using System;
using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface IReferral
  {
    DateTimeOffset CreatedAt { get; set; }
    IUser CreatedByUser { get; set; }
    int CreatedByUserId { get; set; }
    IList<IExamination> Examinations { get; set; }
    IPatient Patient { get; set; }
    int PatientId { get; set; }
    IReferralStatus ReferralStatus { get; set; }
    int ReferralStatusId { get; set; }
  }
}