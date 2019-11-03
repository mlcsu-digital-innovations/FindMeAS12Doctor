using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class UserPostProfile : Profile
    {
        public UserPostProfile()
        {
            CreateMap<UserPost, BusinessModels.User>();
        }
    }
}