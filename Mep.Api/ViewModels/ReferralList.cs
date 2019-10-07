using System;

namespace Mep.Api.ViewModels
{
  public class ReferralList
  {
    public int DoctorsAllocated { get; set; }
    public string ExaminationLocationPostcode { get; set; }
    public string LeadAmhp { get; set; }
    public int NumberOfExaminationAttempts { get; set; }
    public string PatientIdentifier { get; set; }
    public int PatientId { get; set; }
    public int ReferralId { get; set; }
    public int ResponsesReceived { get; set; }
    public string Speciality { get; set; }
    public string Status { get; set; }
    public DateTime? Timescale { get; set; }
  }
} 