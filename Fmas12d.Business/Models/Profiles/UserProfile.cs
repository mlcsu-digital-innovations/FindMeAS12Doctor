using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
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