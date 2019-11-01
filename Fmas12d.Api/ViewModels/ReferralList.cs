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
    public string SpecialityName { get; set; }
    public string StatusName { get; set; }
    public DateTimeOffset? Timescale { get; set; }

    public static Func<Business.Models.Referral, ReferralList> ProjectFromModel
    {
      get
      {
        return referral => new ReferralList()
        {
          DoctorsAllocated = referral.DoctorsAllocated,
          ExaminationLocationPostcode = referral.ExaminationLocationPostcode,
          LeadAmhp = referral.LeadAmhp,
          NumberOfExaminationAttempts = referral.NumberOfExaminationAttempts,
          PatientId = referral.PatientId,
          PatientIdentifier = referral.PatientIdentifier,
          ReferralId = referral.Id,
          ResponsesReceived = referral.ResponsesReceived,
          SpecialityName = referral.SpecialityName,
          StatusName = referral.StatusName,
          Timescale = referral.Timescale
        };
      }
    }     
  }
} 