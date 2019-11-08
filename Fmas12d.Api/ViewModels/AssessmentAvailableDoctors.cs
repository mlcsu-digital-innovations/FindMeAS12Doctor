using System;
using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Api.ViewModels
{
  public class AssessmentAvailableDoctors : AssessmentView
  {
    public AssessmentAvailableDoctors()
    {
    }

    public AssessmentAvailableDoctors(Business.Models.Assessment model) : base(model)
    {
      AmhpName = model.AmhpUserName;
      AvailableDoctors = 
        model.AvailableDoctors
             .Select(ad => new AssessmentAvailableDoctorsAvailability(ad)).ToList();
      LeadAmhpName = model.LeadAmhpName;
      PreferredDoctorGenderTypeName = model.PreferredDoctorGenderTypeName;
    }

    public string AmhpName { get; set; }
    public IEnumerable<AssessmentAvailableDoctorsAvailability> AvailableDoctors { get; set; }
    public string LeadAmhpName { get; set; }
    public string PreferredDoctorGenderTypeName { get; set; }

    public static new Func<Business.Models.Assessment, AssessmentAvailableDoctors> ProjectFromModel
    {
      get
      {
        return model => new AssessmentAvailableDoctors(model);
      }
    }
  }
}