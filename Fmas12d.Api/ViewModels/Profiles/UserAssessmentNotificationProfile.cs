using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
{
    public class UserAssessmentNotificationProfile : Profile
    {
        public UserAssessmentNotificationProfile()
        {
            CreateMap<UserAssessmentNotification, BusinessModels.UserAssessmentNotification>();
            CreateMap<BusinessModels.UserAssessmentNotification, UserAssessmentNotification>();
        }
    }
}