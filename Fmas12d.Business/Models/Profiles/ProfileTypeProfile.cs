using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
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