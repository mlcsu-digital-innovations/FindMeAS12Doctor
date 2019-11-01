using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
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