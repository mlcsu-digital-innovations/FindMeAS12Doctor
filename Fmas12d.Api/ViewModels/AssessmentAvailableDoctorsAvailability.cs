using System;
using System.Linq;
using System.Collections.Generic;

namespace Fmas12d.Api.ViewModels
{
  public class AssessmentAvailableDoctorsAvailability
  {
    public AssessmentAvailableDoctorsAvailability()
    {
    }

    public AssessmentAvailableDoctorsAvailability(Business.Models.IUserAvailabilityDoctor model)
    {
      ActiveAssessments = model.ActiveAssessments.Select(aa => new PostcodeTime(aa)).ToList();
      Distance = model.Distance;
      GenderName = model.GenderName;
      Id = model.Id;
      Name = model.Name;
      SpecialityNames = model.SpecialityNames;
      Type = model.Type;
    }

    public IEnumerable<PostcodeTime> ActiveAssessments { get; set; }
    public decimal Distance { get; set; }
    public int Id { get; set; }
    public string GenderName { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> SpecialityNames { get; set; }
    public string Type { get; set; }    

    public static Func<Business.Models.IUserAvailabilityDoctor, AssessmentAvailableDoctorsAvailability> ProjectFromModel
    {
      get
      {
        return model => new AssessmentAvailableDoctorsAvailability(model);
      }
    }
  }
}