using System;
using System.Linq;
using System.Collections.Generic;

namespace Fmas12d.Api.ViewModels
{
  public class AssessmentSelectedDoctor
  {
    public AssessmentSelectedDoctor()
    {
    }
    public AssessmentSelectedDoctor(Business.Models.AssessmentDoctor model)
    {
      if (model == null) return;

      Distance = model.Distance;
      GenderName = model.DoctorUser?.GenderName;
      HasAccepted = model.HasAccepted ?? false;
      Id = model.DoctorUserId;
      IsAvailable = model.IsAvailable;
      Name = model.DoctorUser?.DisplayName;
      SpecialityNames = model.DoctorUser?.UserSpecialities.Select(us => us.Speciality.Name).ToList();
      Type = model.DoctorUser?.ProfileType?.Name;
    }    

    public decimal? Distance { get; set; }
    public bool HasAccepted { get; set; }
    public int Id { get; set; }
    public bool IsAvailable { get; set; }
    public string GenderName { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> SpecialityNames { get; set; }
    public string Type { get; set; }    

  }
}