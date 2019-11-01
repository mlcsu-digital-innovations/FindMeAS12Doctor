using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
{
    public class UserExaminationNotificationProfile : Profile
    {
        public UserExaminationNotificationProfile()
        {
            CreateMap<UserExaminationNotification, BusinessModels.UserExaminationNotification>();
            CreateMap<BusinessModels.UserExaminationNotification, UserExaminationNotification>();
        }
    }
}