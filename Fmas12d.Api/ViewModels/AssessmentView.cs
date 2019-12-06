using System;
using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Api.ViewModels
{
  public class AssessmentView
  {
    public AssessmentView()
    {}    
    public AssessmentView(Business.Models.Assessment model, bool ignoreSelectedDoctors = false)
    {
      if (model == null) return;

      Address1 = model.Address1;
      Address2 = model.Address2;
      Address3 = model.Address3;
      Address4 = model.Address4;
      CanUpdateOutcome = 
        model.IsSuccessful == null && 
        model.Referral.ReferralStatusId == Business.Models.ReferralStatus.ASSESSMENT_SCHEDULED;
      DateTime = model.DateTime;
      DetailTypes = model.DetailTypes
                         ?.Select(dt => new AssessmentDetailType(dt)).ToList();
      Id = model.Id;
      IsSuccessful = model.IsSuccessful;
      MeetingArrangementComment = model.MeetingArrangementComment;
      PatientIdentifier = model.PatientIdentifier;
      Postcode = model.Postcode;
      PreferredDoctorGenderTypeName = model.PreferredDoctorGenderTypeName;
      ReferralId = model.ReferralId;
      SpecialityName = model.SpecialityName;

      if (model.DoctorsAllocated != null && 
          model.DoctorsAllocated.Any())
      {
        DoctorsAllocated = model.DoctorsAllocated
                                .Select(da => new AssessmentViewDoctor(da)).ToList();
      }

      if (!ignoreSelectedDoctors && 
          model.DoctorsSelected != null && 
          model.DoctorsSelected.Any())
      {
        DoctorsSelected = model.DoctorsSelected
                               .Select(ds => new AssessmentViewDoctor(ds)).ToList();
      }            

    }

    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Address4 { get; set; }
    public bool CanUpdateOutcome { get; set; }
    public DateTimeOffset DateTime { get; set; }
    public IList<AssessmentDetailType> DetailTypes { get; set; }
    public IList<AssessmentViewDoctor> DoctorsAllocated { get; set; }
    public IList<AssessmentViewDoctor> DoctorsSelected { get; set; }
    public int Id { get; set; }
    public bool? IsSuccessful { get; set; }
    public string MeetingArrangementComment { get; set; }
    public string PatientIdentifier { get; set; }    
    public string Postcode { get; set; }
    public string PreferredDoctorGenderTypeName {get; set;}
    public int ReferralId { get; set; }
    public string SpecialityName { get; set; }

    public static Func<Business.Models.Assessment, AssessmentView> ProjectFromModel
    {
      get
      {
        return model => new AssessmentView(model);
      }
    }    
  }
}