using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Fmas12d.Api.ViewModels
{
  public class AssessmentView
  {
    public AssessmentView()
    {}    
    public AssessmentView(Business.Models.Assessment model)
    {
      Address1 = model.Address1;
      Address2 = model.Address2;
      Address3 = model.Address3;
      Address4 = model.Address4;
      DateTime = model.DateTime;
      Id = model.Id;
      IsSuccessful = model.IsSuccessful;
      MeetingArrangementComment = model.MeetingArrangementComment;
      PatientIdentifier = model.PatientIdentifier;
      Postcode = model.Postcode;
      PreferredDoctorGenderTypeName = model.PreferredDoctorGenderTypeName;
      ReferralId = model.ReferralId;
      SpecialityName = model.SpecialityName;

      if (model.DoctorsAllocated != null && model.DoctorsAllocated.Any())
      {
        DoctorsAllocated = new Collection<AssessmentViewDoctor>();
        foreach (var doctorAllocated in model.DoctorsAllocated.OrderBy(d => d.DisplayName))
        {
          DoctorsAllocated.Add(new AssessmentViewDoctor(doctorAllocated));
        }
      }      

    }

    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Address4 { get; set; }
    public DateTimeOffset DateTime { get; set; }
    public IList<AssessmentViewDoctor> DoctorsAllocated { get; set; }
    public int Id { get; set; }
    public bool? IsSuccessful { get; set; }
    public string MeetingArrangementComment { get; set; }    
    public string PatientIdentifier { get; set; }
    public string Postcode { get; set; }
    public string PreferredDoctorGenderTypeName {get; set;}
    public int ReferralId { get; set; }
    public string SpecialityName { get; set; }    
  }
}