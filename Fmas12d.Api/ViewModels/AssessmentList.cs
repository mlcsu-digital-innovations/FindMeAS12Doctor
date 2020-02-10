using System;
using System.Linq;

namespace Fmas12d.Api.ViewModels
{
  public class AssessmentList
  {
    public AssessmentList() {}
    public AssessmentList(Business.Models.Assessment model)
    {
      if (model == null) return;

      DateTime = model.DateTime;
      DoctorStatusId = model.Doctors?.SingleOrDefault()?.StatusId;
      DoctorHasAccepted = model.Doctors?.SingleOrDefault()?.HasAccepted;
      Id = model.Id;
      HasBeenReviewed = model.CompletionConfirmationByUserId.HasValue;
      HasBeenCompleted = model.CompletedByUserId.HasValue;
      PatientId = model.Referral.Patient?.NhsNumber == null 
        ? model.Referral.Patient?.AlternativeIdentifier
        : model.Referral.Patient?.NhsNumber.ToString();
      Postcode = model.Postcode;
      ReferralStatusId = model.Referral.ReferralStatusId;
    }

    public DateTimeOffset DateTime { get; set; }
    public int? DoctorStatusId { get; set; }
    public bool? DoctorHasAccepted { get; set; }
    public bool HasBeenCompleted { get; set; }
    public bool HasBeenReviewed { get; set; }
    public int Id { get; set; }
    public string PatientId {get; set;}
    public string Postcode { get; set; }
    public int ReferralStatusId { get; set; }

    public static Func<Business.Models.Assessment, AssessmentList> ProjectFromModel
    {
      get
      {
        return model => new AssessmentList(model);
      }
    }
  }


}