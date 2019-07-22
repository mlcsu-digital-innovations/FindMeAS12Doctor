using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class UserProfile : Profile
  {
    public UserProfile()
    {
      CreateMap<Entities.User, Models.User>();

      CreateMap<Models.User, Entities.User>();
    }
  }
}