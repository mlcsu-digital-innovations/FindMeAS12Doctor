using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
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