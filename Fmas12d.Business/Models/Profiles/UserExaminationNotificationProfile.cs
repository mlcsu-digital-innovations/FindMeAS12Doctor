using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class UserExaminationNotificationProfile : Profile
  {
    public UserExaminationNotificationProfile()
    {
      CreateMap<Entities.UserExaminationNotification, Models.UserExaminationNotification>();

      CreateMap<Models.UserExaminationNotification, Entities.UserExaminationNotification>();
    }
  }
}