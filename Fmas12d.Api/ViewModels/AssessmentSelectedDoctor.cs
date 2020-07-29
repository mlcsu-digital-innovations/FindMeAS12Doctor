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

      ContactDetails = model.DoctorUser?.ContactDetails.Select(cd => new ContactDetail(cd)).ToList();

      Distance = model.Distance;
      GenderName = model.DoctorUser?.GenderName;
      HasAccepted = model.HasAccepted ?? false;
      HasResponded = model.RespondedAt == null ? false : true;
      Id = model.DoctorUserId;
      IsAvailable = model.IsAvailable;
      Name = model.DoctorUser?.DisplayName;
      SpecialityNames = model.DoctorUser?.UserSpecialities.Select(us => us.Speciality.Name).ToList();
      Type = model.DoctorUser?.ProfileType?.Name;
    }    

    public IEnumerable<ContactDetail> ContactDetails { get; set; }

    public decimal? Distance { get; set; }
    public bool HasAccepted { get; set; }
    public bool HasResponded { get; set; }
    public int Id { get; set; }
    public bool IsAvailable { get; set; }
    public string GenderName { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> SpecialityNames { get; set; }
    public string Type { get; set; }    

  }
}