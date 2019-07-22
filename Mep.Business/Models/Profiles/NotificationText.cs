using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class NotificationTextProfile : Profile
  {
    public NotificationTextProfile()
    {
      CreateMap<Entities.NotificationText, Models.NotificationText>();

      CreateMap<Models.NotificationText, Entities.NotificationText>();
    }
  }
}