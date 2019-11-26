using System;
using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Api.ViewModels
{
  public class AssessmentSelectedDoctors : AssessmentView
  {
    public AssessmentSelectedDoctors()
    {
    }

    public AssessmentSelectedDoctors(Business.Models.Assessment model) : base(model, true)
    {
      AmhpName = model.AmhpUserName;

      if (model.DoctorsSelected != null && 
          model.DoctorsSelected.Any())
      {
        DoctorsSelected = model.Doctors
                               .Where(d => d.IsSelected)
                               .Select(d => new AssessmentSelectedDoctor(d))
                               .ToList();
      }     

      LeadAmhpName = model.LeadAmhpName;
      PreferredDoctorGenderTypeName = model.PreferredDoctorGenderTypeName;
    }

    public string AmhpName { get; set; }
    public new IEnumerable<AssessmentSelectedDoctor> DoctorsSelected { get; set; }
    public string LeadAmhpName { get; set; }

    public static new Func<Business.Models.Assessment, AssessmentAvailableDoctors> ProjectFromModel
    {
      get
      {
        return model => new AssessmentAvailableDoctors(model);
      }
    }
  }
}