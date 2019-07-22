using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class ProfileTypeProfile : Profile
  {
    public ProfileTypeProfile()
    {
      CreateMap<Entities.ProfileType, Models.ProfileType>();

      CreateMap<Models.ProfileType, Entities.ProfileType>();
    }
  }
}