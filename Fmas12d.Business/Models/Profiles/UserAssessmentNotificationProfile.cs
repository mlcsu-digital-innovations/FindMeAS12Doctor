using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class UserAssessmentNotificationProfile : Profile
  {
    public UserAssessmentNotificationProfile()
    {
      CreateMap<Entities.UserAssessmentNotification, Models.UserAssessmentNotification>();

      CreateMap<Models.UserAssessmentNotification, Entities.UserAssessmentNotification>();
    }
  }
}