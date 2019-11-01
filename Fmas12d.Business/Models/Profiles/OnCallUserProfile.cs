using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
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