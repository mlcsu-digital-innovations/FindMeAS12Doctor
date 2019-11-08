using System;

namespace Fmas12d.Api.ViewModels
{
  public class ReferralList
  {    
    public string AssessmentLocationPostcode { get; set; }
    public int? CurrentAssessmentId { get; set; }
    public int DoctorsAllocated { get; set; }
    public string LeadAmhp { get; set; }
    public int NumberOfAssessmentAttempts { get; set; }
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
          AssessmentLocationPostcode = referral.AssessmentLocationPostcode,
          CurrentAssessmentId = referral?.CurrentAssessment?.Id,
          DoctorsAllocated = referral.DoctorsAllocated,
          LeadAmhp = referral.LeadAmhp,
          NumberOfAssessmentAttempts = referral.NumberOfAssessmentAttempts,
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