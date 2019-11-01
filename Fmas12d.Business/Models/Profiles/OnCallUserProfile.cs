using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class OnCallUserProfile : Profile
  {
    public OnCallUserProfile()
    {
      CreateMap<Entities.OnCallUser, Models.OnCallUser>();

      CreateMap<Models.OnCallUser, Entities.OnCallUser>();
    }
  }
}