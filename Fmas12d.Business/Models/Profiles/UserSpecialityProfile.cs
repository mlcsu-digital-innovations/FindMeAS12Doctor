using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class UserSpecialityProfile : Profile
  {
    public UserSpecialityProfile()
    {
      CreateMap<Entities.UserSpeciality, Models.UserSpeciality>();

      CreateMap<Models.UserSpeciality, Entities.UserSpeciality>();
    }
  }
}