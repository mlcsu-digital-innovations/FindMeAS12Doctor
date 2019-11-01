using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
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