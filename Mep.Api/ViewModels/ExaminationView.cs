using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mep.Api.ViewModels
{
  public class ExaminationView
  {
    public ExaminationView()
    {}    
    public ExaminationView(Business.Models.Examination model)
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
      ReferralId = model.ReferralId;
      SpecialityName = model.SpecialityName;

      if (model.DoctorsAllocated != null && model.DoctorsAllocated.Any())
      {
        DoctorsAllocated = new Collection<ExaminationViewDoctor>();
        foreach (var doctorAllocated in model.DoctorsAllocated.OrderBy(d => d.DisplayName))
        {
          DoctorsAllocated.Add(new ExaminationViewDoctor(doctorAllocated));
        }
      }      

    }

    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Address4 { get; set; }
    public DateTimeOffset DateTime { get; set; }
    public IList<ExaminationViewDoctor> DoctorsAllocated { get; set; }
    public int Id { get; set; }
    public bool? IsSuccessful { get; set; }
    public string MeetingArrangementComment { get; set; }    
    public string PatientIdentifier { get; set; }
    public string Postcode { get; set; }
    public int ReferralId { get; set; }
    public string SpecialityName { get; set; }    
  }
}