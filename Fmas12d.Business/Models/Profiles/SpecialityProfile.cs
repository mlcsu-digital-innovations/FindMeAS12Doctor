using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class SpecialityProfile : Profile
  {
    public SpecialityProfile()
    {
      CreateMap<Entities.Speciality, Models.Speciality>();

      CreateMap<Models.Speciality, Entities.Speciality>();
    }
  }
}