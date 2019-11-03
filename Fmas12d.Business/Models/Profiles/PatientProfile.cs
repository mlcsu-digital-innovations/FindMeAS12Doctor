using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class PatientProfile : Profile
  {
    public PatientProfile()
    {
      CreateMap<Entities.Patient, Models.Patient>();

      CreateMap<Models.Patient, Entities.Patient>();
    }
  }
}