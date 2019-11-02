using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
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