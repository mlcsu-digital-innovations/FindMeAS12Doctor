using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
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